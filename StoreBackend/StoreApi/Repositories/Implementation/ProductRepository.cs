using StoreApi.Enums;
using StoreApi.Models;
using StoreDatabase;
using StoreDatabase.Enums;
using StoreDatabase.Models;
using System.Data.Entity;

namespace StoreApi.Repositories.Implementation
{
	public class ProductRepository : IProductRepository
	{
		private readonly int NameMaxLength = 100;

		public async Task<ProductDetails> AddOrUpdateProductAsync(ProductDetails model)
		{
			if (await ValidateModelAsync(model))
			{
				using (var context = new StoreDatabaseContext())
				{
					var newProduct = model.Id == 0;
					var product = newProduct
						? new Product()
						: await context.Products.FirstAsync(p => p.Id == model.Id);

					product.Name = model.Name;
					product.CategoryId = model.CategoryId;
					product.Status = MapStatusToDatabase(model.Status);
					product.Price = model.Price;
					product.Image = model.Image;

					if (newProduct)
					{
						context.Products.Add(product);

						await context.SaveChangesAsync();

						model.Id = product.Id;
					}

					var inventory = await context.Inventory.FirstOrDefaultAsync(p => p.ProductId == product.Id);
					if (inventory == null)
					{
						inventory = new Inventory
						{
							ProductId = product.Id
						};

						context.Inventory.Add(inventory);
					}

					inventory.StoredQuantity = model.Quantity ?? 0;

					await context.SaveChangesAsync();
				}
			}

			return model;
		}

		private async Task<bool> ValidateModelAsync(ProductDetails model)
		{
			model.Error = null;

			if (model.Id == 0 && model.Status != ProductStatus.Draft)
			{
				model.Error = ProductError.NewProductStatusNotDraft;
			}
			else if (string.IsNullOrEmpty(model.Name))
			{
				model.Error = ProductError.NameTooShort;
			}
			else if (model.Name != model.Name.Trim())
			{
				model.Error = ProductError.NameBeginsOrEndsWithWhitespace;
			}
			else if (model.Name.Length > NameMaxLength)
			{
				model.Error = ProductError.NameTooLong;
			}
			else if (model.Status == ProductStatus.Available && model.Quantity == 0 || model.Quantity < 0)
			{
				model.Error = ProductError.QuantityOutOfRange;
			}
			else if (model.Status != ProductStatus.Draft && model.Price <= 0)
			{
				model.Error = ProductError.PriceOutOfRange;
			}
			else
			{
				using (var context = new StoreDatabaseContext())
				{
					if (context.Products.Any(p => p.Name == model.Name && p.Id != model.Id))
					{
						model.Error = ProductError.NameAlreadyExists;
					}
					else if (!context.Categories.Any(c => c.Id == model.CategoryId))
					{
						model.Error = ProductError.IncorrectCategory; 
					}
					else if (model.Id != 0)
					{
						var original = await context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

						if (original == null)
						{
							model.Error = ProductError.OriginalProductNotFound;
						}
						else if (original.Status != Status.Draft && model.Status == ProductStatus.Draft)
						{
							model.Error = ProductError.StatusChangeBackToDraft;
						}
						else if (original.Status == Status.Removed && model.Status != ProductStatus.Removed)
						{
							model.Error = ProductError.StatusChangeFromRemoved;
						}
					}
				}
			}

			return model.Error == null;
		}

		public async Task<ProductDetails?> GetProductAsync(int id, bool fullInfo)
		{
			using (var context = new StoreDatabaseContext())
			{
				var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
				if (product == null)
				{
					return null;
				}

				if (fullInfo)
				{
					var inventory = await context.Inventory.FirstOrDefaultAsync(x => x.ProductId == id);

					return MapToModel(product, inventory);
				}
				else
				{
					return MapToModel(product);
				}
			}
		}

		private ProductDetails MapToModel(Product product, Inventory? inventory = null, ProductError? error = null)
		{
			return new ProductDetails
			{
				Id = product.Id,
				Name = product.Name,
				CategoryId = product.CategoryId,
				CategoryName = product.Category.Name,
				Price = product.Price,
				Status = MapStatusToModel(product.Status),
				Quantity = inventory?.StoredQuantity,
				Image = product.Image,
				Error = error
			};
		}

		private ProductStatus MapStatusToModel(Status status)
		{
			return status switch
			{
				Status.Draft => ProductStatus.Draft,
				Status.Available => ProductStatus.Available,
				Status.NotAvailable => ProductStatus.NotAvailable,
				Status.Removed => ProductStatus.Removed,
				_ => throw new NotImplementedException()
			};
		}

		private Status MapStatusToDatabase(ProductStatus status)
		{
			return status switch
			{
				ProductStatus.Draft => Status.Draft,
				ProductStatus.Available => Status.Available,
				ProductStatus.NotAvailable => Status.NotAvailable,
				ProductStatus.Removed => Status.Removed,
				_ => throw new NotImplementedException()
			};
		}
	}
}
