using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Client.Contact
{
	public class ClientContact : Base.ModelBase
	{
		[Required]
		public Client Client { get; set; }
		[Required]
		public ClientContactType Type { get; set; }
		[Required]
		public string Value { get; set; }

	}
}
