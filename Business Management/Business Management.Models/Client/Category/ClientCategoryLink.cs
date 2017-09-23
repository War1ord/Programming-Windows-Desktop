using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Client.Category
{
	public class ClientCategoryLink : Base.ModelBase
	{
		[Required]
		public Client Client { get; set; }
		[Required]
		public ClientCategory Category { get; set; }

	}
}
