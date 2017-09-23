using System;
using StockSystem.Data;
using StockSystem.Models;

namespace StockSystem.Business
{
	public abstract class Base : IDisposable
	{
		protected Data.DataContext Db { get; private set; }

		public Result ErrorUnknown { get { return new Result("Unknown Error", ResultType.Error); } }
		public Result Success { get { return new Result("Saved", ResultType.Success); } }

		protected Base() { Db = new DataContext(); }

		public void Dispose()
		{
			Db.Dispose();
			Db = null;
		}
	}
}