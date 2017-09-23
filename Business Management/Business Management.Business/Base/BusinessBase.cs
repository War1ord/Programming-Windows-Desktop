using Business_Management.Business.Extentions;
using Business_Management.Data;
using System;

namespace Business_Management.Business.Base
{
	public abstract class BusinessBase : IBusinessBase, IDisposable
	{
		private DataContext _db;
		protected DataContext Db { get { return _db ?? (_db = new DataContext()); } }

		public void Dispose()
		{
			_db.SafeDispose();
			_db = null;
		}
	}

}
