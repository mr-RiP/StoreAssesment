namespace StoreApi.Models
{
	public class CartReport
	{
		public bool AllClear { get; set; }

		public IReadOnlyCollection<CartReportItem>? MissingItems { get; set; }
	}
}
