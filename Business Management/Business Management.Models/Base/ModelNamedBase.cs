using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Base
{
	public abstract class ModelNamedBase : ModelBase
	{
		[Required, StringLength(100)]
		public string Name { get; set; }

		public override string ToString()
		{
			return !string.IsNullOrWhiteSpace(Name) ? Name : "";
		}

	}
}
