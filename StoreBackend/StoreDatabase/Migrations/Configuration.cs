using StoreDatabase.Initializers;
using System.Data.Entity.Migrations;

namespace StoreDatabase.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<StoreDatabase.StoreDatabaseContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(StoreDatabaseContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data.

			var initializer = new TestDataInitializer();
			initializer.Seed(context);
		}
	}
}
