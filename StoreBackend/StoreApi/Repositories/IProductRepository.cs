using StoreApi.Models;

namespace StoreApi.Repositories
{
	public interface IProductRepository
	{
		Task<ProductDetails?> GetProductAsync(int id, bool fullInfo);

		Task<ProductDetails> AddOrUpdateProductAsync(ProductDetails model);
	}
}
