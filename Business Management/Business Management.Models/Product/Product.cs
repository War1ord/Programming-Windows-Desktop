using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Product
{
	public class Product : Base.ModelNamedBase
	{
		[Required]
		public ProductType Type { get; set; }
		public string Description { get; set; }
		[Required]
		public decimal Amount { get; set; }
	}
}
