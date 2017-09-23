using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Service
{
	public class Service : Base.ModelNamedBase
	{
		[Required]
		public ServiceType Type { get; set; }
		public string Description { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public ServiceInterval Interval { get; set; }
	}
}
