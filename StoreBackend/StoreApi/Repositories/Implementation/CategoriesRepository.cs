using StoreApi.Models;
using StoreDatabase;
using System.Data.Entity;

namespace StoreApi.Repositories.Implementation
{
	public class CategoriesRepository : ICategoriesRepository
	{
		public async Task<CategoryList> GetAllAsync()
		{
			using (var context = new StoreDatabaseContext())
			{
				var items = await context.Categories
					.Select(c => new CategoryListItem { Id = c.Id, Name = c.Name })
					.ToArrayAsync();

				return new CategoryList { Items = items };
			}
		}
	}
}
