using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracking.Models
{
	public class ModelBase
	{
		private Guid? _guid;
		private DateTime? _createdDate;

		[Key]
		public Guid Guid
		{
			get { return (_guid ?? (_guid = Guid.NewGuid())).Value; }
			set { _guid = value; }
		}
		public DateTime CreatedDate
		{
			get { return (_createdDate ?? (_createdDate = DateTime.Now)).Value; }
			set { _createdDate = value; }
		}
		public bool IsDeleted { get; set; }
	}
}