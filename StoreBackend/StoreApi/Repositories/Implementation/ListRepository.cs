using StoreApi.Enums;
using StoreApi.Models;
using StoreDatabase;
using StoreDatabase.Enums;
using StoreDatabase.Models;
using System.Data.Entity;

namespace StoreApi.Repositories.Implementation
{
	public class ListRepository : IListRepository
	{
		public ProductList GetProductListByIds(int[] ids)
		{
			if (ids == null || ids.Length == 0)
			{
				return new ProductList();
			}

			using (var context = new StoreDatabaseContext())
			{
				var queryable = SelectProductsByIds(context, ids);
				var products = queryable.ToArray();
				var listItems = MapProductListItems(products);

				return new ProductList { Items = listItems };
			}
		}

		public async Task<ProductList> GetProductListByIdsAsync(int[] ids)
		{
			if (ids == null || ids.Length == 0)
			{
				return new ProductList();
			}

			using (var context = new StoreDatabaseContext())
			{
				var queryable = SelectProductsByIds(context, ids);
				var products = await queryable.ToArrayAsync();
				var listItems = MapProductListItems(products);

				return new ProductList { Items = listItems };
			}
		}

		private IQueryable<Product> SelectProductsByIds(StoreDatabaseContext context, int[] ids)
		{
			var distinctIds = ids.Distinct().ToArray();
			var queryable = context.Products.Where(p => distinctIds.Contains(p.Id));

			return queryable;
		}

		private IReadOnlyCollection<ProductListItem> MapProductListItems(IReadOnlyCollection<Product> products)
		{
			var productListItems = products
				.Select(p => new ProductListItem
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					IsAvailable = p.Status == Status.Available
				})
				.ToArray();

			return productListItems;
		}

		public ProductList GetProductListByPage(int page, int pageSize, bool availableOnly, int? categoryId, ListSort sort)
		{
			if (page < 1 || pageSize < 1)
			{
				return new ProductList();
			}

			using (var context = new StoreDatabaseContext())
			{
				var selection = SelectProductsByFilters(context, availableOnly, categoryId);
				var count = selection.Count();

				var sortedSelection = ApplySort(selection, sort);
				var pagedSelection = ApplyPagination(sortedSelection, page, pageSize);
				var products = pagedSelection.ToArray();

				var model = GetProductList(products, count, page, pageSize);

				return model;
			}
		}

		public async Task<ProductList> GetProductListByPageAsync(int page, int pageSize, bool availableOnly, int? categoryId, ListSort sort)
		{
			if (page < 1 || pageSize < 1)
			{
				return new ProductList();
			}

			using (var context = new StoreDatabaseContext())
			{
				var selection = SelectProductsByFilters(context, availableOnly, categoryId);
				var count = await selection.CountAsync();

				var sortedSelection = ApplySort(selection, sort);
				var pagedSelection = ApplyPagination(sortedSelection, page, pageSize);
				var products = await pagedSelection.ToArrayAsync();

				var model = GetProductList(products, count, page, pageSize);

				return model;
			}
		}

		private IQueryable<Product> SelectProductsByFilters(StoreDatabaseContext context, bool availableOnly, int? categoryId)
		{
			IQueryable<Product> selection = context.Products;

			if (availableOnly)
			{
				selection = selection.Where(p => p.Status == Status.Available);
			}
			if (categoryId != null)
			{
				selection = selection.Where(p => p.CategoryId == categoryId);
			}

			return selection;
		}

		private IQueryable<Product> ApplyPagination(IQueryable<Product> selection, int page, int pageSize)
		{
			return selection
				.Skip((page - 1) * pageSize)
				.Take(pageSize);
		}

		private IQueryable<Product> ApplySort(IQueryable<Product> selection, ListSort sort)
		{
			return sort switch
			{
				ListSort.NameAsc => selection.OrderBy(p => p.Name),
				ListSort.NameDesc => selection.OrderByDescending(p => p.Name),
				ListSort.PriceAsc => selection.OrderBy(p => p.Price),
				ListSort.PriceDesc => selection.OrderByDescending(p => p.Price),
				_ => throw new NotImplementedException()
			};
		}

		private int GetCorrectPage(int initialPage)
		{
			return Math.Max(initialPage, 1);
		}

		private ProductList GetProductList(IReadOnlyCollection<Product> products, int totalCount, int page, int pageSize)
		{
			var items = MapProductListItems(products);
			var total = (int) Math.Ceiling((double)totalCount / pageSize);

			return new ProductList { Items = items, Page = page, Total = total };
		}
	}
}
