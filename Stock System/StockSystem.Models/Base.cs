using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockSystem.Models
{
	public abstract class Base
	{
		private Guid _id;

		private Result _result;

		public Guid Id
		{
			get { return _id != Guid.Empty ? _id : (_id = Guid.NewGuid()); }
			set { _id = value; }
		}

		[NotMapped]
		public Result Result
		{
			get { return _result ?? (_result = new Result()); }
			set { _result = value; }
		}

		public bool IsDeleted { get; set; }
	}
}