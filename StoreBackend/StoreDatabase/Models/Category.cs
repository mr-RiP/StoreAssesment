using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDatabase.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Index(IsUnique = true)]
		[MaxLength(100)]
		[MinLength(1)]
		public string Name { get; set; } = string.Empty;

		public virtual ICollection<Product> Products { get; set; } = null!;
	}
}
