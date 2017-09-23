using System;
using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Financial
{
	public class ServiceTimeLog : Base.ModelBase
	{
		[Required]
		public User.User User { get; set; }
		[Required]
		public Client.Client Client { get; set; }
		[Required]
		public Service.Service Service { get; set; }
		[Required]
		public DateTime Started { get; set; }
		public DateTime? Ended { get; set; }
		public bool? Current { get; set; }
		public decimal Amount { get; set; }
		public Invoice Invoice { get; set; }
	}
}
