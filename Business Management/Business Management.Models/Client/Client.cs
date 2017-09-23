using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Client
{
	public class Client : Base.ModelBase
	{
		public General.Title Title { get; set; }
		[Required]
		public string FirstName { get; set; }
		public string MiddleNames { get; set; }
		public string LastName { get; set; }
		public Enums.Gender Gender { get; set; }
	}
}
