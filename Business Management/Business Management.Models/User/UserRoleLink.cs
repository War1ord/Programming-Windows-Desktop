using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.User
{
	public class UserRoleLink : Base.ModelBase
	{
		[Required]
		public User User { get; set; }
		[Required]
		public UserRole Role { get; set; }
	}
}
