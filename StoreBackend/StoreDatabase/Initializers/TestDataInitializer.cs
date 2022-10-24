using StoreDatabase.Enums;
using StoreDatabase.Models;
using System.Data.Entity.Migrations;

namespace StoreDatabase.Initializers
{
	internal class TestDataInitializer
	{
		private static readonly int CategoryMilk = 1;
		private static readonly int CategoryMeat = 2;
		private static readonly int CategoryDrinks = 3;
		private static readonly int CategoryBread = 4;

		private static readonly int ProductMilk32 = 1;
		private static readonly int ProductMilk25 = 2;
		private static readonly int ProductRaspberryYogurt = 3;
		private static readonly int ProductSteak = 4;
		private static readonly int ProductSausages = 5;
		private static readonly int ProductCocaCola = 6;
		private static readonly int ProductSprite = 7;

		internal void Seed(StoreDatabaseContext context)
		{
			var categories = new Category[]
			{
				new Category { Id = CategoryMilk, Name = "Молочные продукты" },
				new Category { Id = CategoryMeat, Name = "Мясо и колбасы" },
				new Category { Id = CategoryDrinks, Name = "Напитки" },
				new Category { Id = CategoryBread, Name = "Хлеб и выпечка" }
			};

			var products = new Product[]
			{
				new Product { Id = ProductMilk32, Name = "Молоко 3,2%", CategoryId = CategoryMilk, Status = Status.Available, Price = 654 },
				new Product { Id = ProductMilk25, Name = "Молоко 2,0%", CategoryId = CategoryMilk, Status = Status.NotAvailable, Price = 515 },
				new Product { Id = ProductRaspberryYogurt, Name = "Йогурт клубничный", CategoryId = CategoryMilk, Status = Status.Available, Price = 225 },
				new Product { Id = ProductSteak, Name = "Стейк из говядины", CategoryId = CategoryMeat, Status = Status.Available, Price = 2119 },
				new Product { Id = ProductSausages, Name = "Сосиски конские", CategoryId = CategoryMeat, Status = Status.Available, Price = 1050 },
				new Product { Id = ProductCocaCola, Name = "Coca-Cola Classic", CategoryId = CategoryDrinks, Status = Status.Available, Price = 609 },
				new Product { Id = ProductSprite, Name = "Sprite", CategoryId = CategoryDrinks, Status = Status.Draft, Price = 619 }
			};

			var inventory = new Inventory[]
			{
				new Inventory { Id = ProductMilk32, ProductId = ProductMilk32, StoredQuantity = 15 },
				new Inventory { Id = ProductMilk25, ProductId = ProductMilk25, StoredQuantity = 0 },
				new Inventory { Id = ProductRaspberryYogurt, ProductId = ProductRaspberryYogurt, StoredQuantity = 200 },
				new Inventory { Id = ProductSteak, ProductId = ProductSteak, StoredQuantity = 3 },
				new Inventory { Id = ProductSausages, ProductId = ProductSausages, StoredQuantity = 40 },
				new Inventory { Id = ProductCocaCola, ProductId = ProductCocaCola, StoredQuantity = 50 },
				new Inventory { Id = ProductSprite, ProductId = ProductSprite, StoredQuantity = 50 }
			};

			context.Categories.AddOrUpdate(categories);
			context.Products.AddOrUpdate(products);
			context.Inventory.AddOrUpdate(inventory);
		}
	}
}
