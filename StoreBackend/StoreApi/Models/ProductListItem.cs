namespace StoreApi.Models
{
	public class ProductListItem
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }

		public bool IsAvailable { get; set; }
	}
}
