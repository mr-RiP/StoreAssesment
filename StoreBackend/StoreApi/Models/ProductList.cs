namespace StoreApi.Models
{
	public class ProductList
	{
		public IReadOnlyCollection<ProductListItem> Items { get; set; } = Array.Empty<ProductListItem>();

		public int? Page { get; set; }
		public int? Total { get; set; }
	}
}
