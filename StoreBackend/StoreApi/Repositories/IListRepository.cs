using StoreApi.Enums;
using StoreApi.Models;

namespace StoreApi.Repositories
{
	public interface IListRepository
	{
		ProductList GetProductListByPage(int page, int pageSize, bool availableOnly, int? categoryId, ListSort sort);
		Task<ProductList> GetProductListByPageAsync(int page, int pageSize, bool availableOnly, int? categoryId, ListSort sort);

		ProductList GetProductListByIds(int[] ids);
		Task<ProductList> GetProductListByIdsAsync(int[] ids);
	}
}
