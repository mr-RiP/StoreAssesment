namespace StoreApi.Models
{
	public class CartReportItem
	{
		public int Id { get; set; }

		public int OrderedQuantity { get; set; }

		public bool IsAvailable { get; set; }

		public int? RemainingQuantity { get; set; }
	}
}
