using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.General
{
	public class Title : Base.ModelBase
	{
		[Required, StringLength(100)]
		public string Value { get; set; }

	}
}
