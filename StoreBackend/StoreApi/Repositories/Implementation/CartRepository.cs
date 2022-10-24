using StoreApi.Models;
using StoreDatabase;
using StoreDatabase.Enums;
using System.Data.Entity;

namespace StoreApi.Repositories.Implementation
{
	public class CartRepository : ICartRepository
	{
		public async Task<CartReport> CheckAsync(IReadOnlyCollection<CartItem> items)
		{
			var requestDictionary = GetRequestDictionary(items);
			var result = await CheckByRequestDictionaryAsync(requestDictionary);

			return result;
		}

		private async Task<CartReport> CheckByRequestDictionaryAsync(IReadOnlyDictionary<int, int> requestDictionary)
		{
			using (var context = new StoreDatabaseContext())
			{
				var inventoryItems = await context.Inventory
					.Where(i => requestDictionary.Keys.Contains(i.ProductId))
					.Where(i => i.Product.Status == Status.Available)
					.ToArrayAsync();

				var missingRequestItems = requestDictionary.Keys
					.Except(inventoryItems.Select(i => i.ProductId))
					.Select(i => new CartReportItem { Id = i, OrderedQuantity = requestDictionary[i], IsAvailable = false })
					.ToArray();

				var missingInventoryItems = inventoryItems
					.Where(i => requestDictionary[i.ProductId] > i.StoredQuantity)
					.Select(i => new CartReportItem { Id = i.ProductId, OrderedQuantity = requestDictionary[i.ProductId], IsAvailable = true, RemainingQuantity = i.StoredQuantity })
					.ToArray();

				var missingItems = missingRequestItems
					.Concat(missingInventoryItems)
					.ToArray();

				return missingItems.Length == 0
					? new CartReport { AllClear = true }
					: new CartReport { AllClear = false, MissingItems = missingItems };
			}
		}

		public async Task<CartReport> OrderAsync(IReadOnlyCollection<CartItem> items)
		{
			var requestDictionary = GetRequestDictionary(items);
			var result = await CheckByRequestDictionaryAsync(requestDictionary);

			if (!result.AllClear)
			{
				return result;
			}

			using (var context = new StoreDatabaseContext())
			{
				await context.Inventory
					.Where(i => requestDictionary.Keys.Contains(i.ProductId))
					.ForEachAsync(i => i.StoredQuantity -= requestDictionary[i.ProductId]);

				await context.Products
					.Where(p => p.Status == Status.Available)
					.Join(context.Inventory.Where(i => i.StoredQuantity == 0), p => p.Id, i => i.ProductId, (p, i) => p)
					.ForEachAsync(p => p.Status = Status.NotAvailable);

				await context.SaveChangesAsync();

				return result;
			}
		}

		private IReadOnlyDictionary<int, int> GetRequestDictionary(IReadOnlyCollection<CartItem> items)
		{
			if (items.Count == 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			var itemDictionary = items
				.GroupBy(i => i.Id)
				.ToDictionary(g => g.Key, g => g.Sum(i => i.Quantity));

			if (itemDictionary.Count != items.Count || itemDictionary.Values.Any(v => v <= 0))
			{
				throw new ArgumentOutOfRangeException();
			}

			return itemDictionary;
		}
	}
}
