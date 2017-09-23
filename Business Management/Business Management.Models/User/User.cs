using Business_Management.Models.General;
using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.User
{
	public class User : Base.ModelBase
	{
		[Required, StringLength(50)]
		public string Username { get; set; }
		[Required, StringLength(50)]
		public string Password { get; set; }
		public Title Title { get; set; }
		[Required]
		public string FirstName { get; set; }
		public string MiddleNames { get; set; }
		[StringLength(50)]
		public string LastName { get; set; }
		public Enums.Gender Gender { get; set; }

		public static class sysadmin
		{
			public static string username = "sysadmin";
			public static string password = "sysadmin@#*!_+";
		}

	}
}
