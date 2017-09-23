using System;

namespace TimeTracking.Business.Base
{
	public abstract class BusinessBase : IDisposable
	{
		private TimeTracking.Data.DataContext _db;
		protected TimeTracking.Data.DataContext Db
		{
			get { return _db ?? (_db = new TimeTracking.Data.DataContext()); }
		}

		protected Guid? UserId { get; private set; }

		protected BusinessBase(Guid? userId) { UserId = userId; }
		protected BusinessBase(TimeTracking.Data.DataContext db, Guid? userId) { _db = db; UserId = userId; }

		/*internal void LogException(Exception exception)
		{
			using (Errors.ErrorLogs errorLogs = new Errors.ErrorLogs(exception, UserId))
			{
				var result = errorLogs.Save();
				//if not successful log to event viewer and to a website log file.
				if (!result.IsSuccessful)
				{
					//TODO: log to event viewer and to a website log file
				}
			}
		}*/

		public void Dispose()
		{
			if (_db != null)
			{
				_db.Dispose();
				_db = null;
			}
		}

	}
}