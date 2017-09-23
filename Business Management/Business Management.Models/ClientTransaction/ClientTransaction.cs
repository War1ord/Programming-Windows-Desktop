using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.ClientTransaction
{
	public class ClientTransaction : Base.ModelBase
	{
		[Required]
		public Client.Client Client { get; set; }
		[Required]
		public ClientTransactionType Type { get; set; }
		[Required]
		public decimal Amount { get; set; }
		public Bank.Bank Bank { get; set; }
	}
}
