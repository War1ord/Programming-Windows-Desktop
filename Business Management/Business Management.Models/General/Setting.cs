using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.General
{
	public class Setting : Base.ModelNamedBase
	{
		[Required]
		public string Value { get; set; }

	}
}
