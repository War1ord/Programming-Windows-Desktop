using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Client.Address
{
	public class ClientAddress : Base.ModelBase
	{
		[Required]
		public Client Client { get; set; }
		[Required]
		public ClientAddressType Type { get; set; }

		[Required]
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string AddressLine3 { get; set; }
		public string AddressLine4 { get; set; }
		public string AddressLine5 { get; set; }

	}
}
