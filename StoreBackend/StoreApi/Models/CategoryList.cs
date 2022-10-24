namespace StoreApi.Models
{
	public class CategoryList
	{
		public IReadOnlyCollection<CategoryListItem> Items { get; set; } = Array.Empty<CategoryListItem>();
	}
}
