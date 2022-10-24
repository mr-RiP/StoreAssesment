using StoreApi.Models;

namespace StoreApi.Repositories
{
	public interface ICategoriesRepository
	{
		Task<CategoryList> GetAllAsync();
	}
}
