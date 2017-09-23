using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Client.Category
{
	public class ClientCategory : Base.ModelNamedBase
	{
		[Required]
		public string Description { get; set; }

	}
}
