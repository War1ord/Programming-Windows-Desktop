using System;
using System.ComponentModel.DataAnnotations;

namespace Business_Management.Models.Base
{
	public abstract class ModelBase
	{
		public Guid? _id;
		public DateTime? _created;

		[Required, Key]
		public Guid Id { get { return (_id ?? (_id = Guid.NewGuid())).Value; } set { _id = value; } }
		[Required]
		public DateTime Created { get { return (_created ?? (_created = DateTime.Now)).Value; } set { _created = value; } }

		[Required]
		public User.User CreatedBy { get; set; }

		public DateTime? Modified { get; set; }
		public User.User ModifiedBy { get; set; }

		public bool IsActive { get; set; }


	}
}
