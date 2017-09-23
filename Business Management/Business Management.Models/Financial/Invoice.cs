using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Financial
{
	public class Invoice : Base.ModelBase
	{
		[Required]
		public Client.Client Client { get; set; }
		[Required]
		public decimal Amount { get; set; }
		public bool Paid { get; set; }
		public ClientTransaction.ClientTransaction Transaction { get; set; }
	}
}
