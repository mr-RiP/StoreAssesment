using StoreDatabase.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDatabase.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		[MinLength(1)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }

		[Required]
		public Status Status { get; set; }

		[Required]
		public decimal Price { get; set; }

		[MaxLength(50)]
		public string? Image { get; set; }

		public virtual Category Category { get; set; } = null!;
	}
}
