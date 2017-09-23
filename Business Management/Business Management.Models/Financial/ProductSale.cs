using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Financial
{
	public class ProductSale : Base.ModelBase
	{
		[Required]
		public User.User User { get; set; }
		[Required]
		public Product.Product Product { get; set; }
		[Required]
		public Client.Client Client { get; set; }
		[Required]
		public decimal Amount { get; set; }
		public Invoice Invoice { get; set; }
	}
}
