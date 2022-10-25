using StoreApi.Enums;

namespace StoreApi.Models
{
	public class ProductDetails
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public int CategoryId { get; set; }

		public string? CategoryName { get; set; }

		public ProductStatus Status { get; set; }

		public decimal Price { get; set; }

		public int? Quantity { get; set; }

		public string? Image { get; set; }

		public ProductError? Error { get; set; }

	}
}
