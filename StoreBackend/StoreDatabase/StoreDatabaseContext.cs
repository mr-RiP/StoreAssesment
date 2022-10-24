using StoreDatabase.Initializers;
using StoreDatabase.Models;
using System.Data.Entity;

namespace StoreDatabase
{
	public sealed class StoreDatabaseContext : DbContext
	{
		public StoreDatabaseContext() : base("Server = localhost; Database = StoreDB; Trusted_Connection = True;")
		{
		}

		public DbSet<Product> Products { get; set; } = null!;
		public DbSet<Category> Categories { get; set; } = null!;
		public DbSet<Inventory> Inventory { get; set; } = null!;
	}
}