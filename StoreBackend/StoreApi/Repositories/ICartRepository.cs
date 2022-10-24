using StoreApi.Models;

namespace StoreApi.Repositories
{
	public interface ICartRepository
	{
		Task<CartReport> CheckAsync(IReadOnlyCollection<CartItem> items);

		Task<CartReport> OrderAsync(IReadOnlyCollection<CartItem> items);
	}
}
