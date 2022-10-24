using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDatabase.Models
{
	public class Inventory
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Product))]
		[Index(IsUnique = true)]
		public int ProductId { get; set; }

		[Required]
		public int StoredQuantity { get; set; }

		public virtual Product Product { get; set; } = null!;
	}
}
