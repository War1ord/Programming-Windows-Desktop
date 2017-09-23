using System;
using Work_Tracker.Business.Models;
using Work_Tracker.Data;

namespace Work_Tracker.Business
{
    public abstract class ServiceBase : ObjectBase, IDisposable
    {
        /// <summary>
        /// The database.
        /// </summary>
        private WorkTrackerDataContext _db;

        /// <summary>
        /// The _result
        /// </summary>
        private Result _result;

        /// <summary>
        /// The database.
        /// </summary>
        public WorkTrackerDataContext Db
        {
            get
            {
                return _db ?? (_db = new WorkTrackerDataContext());
            }
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public Result Result
        {
            get { return _result ?? (_result = new Result(ResultType.None, "")); }
            set { _result = value; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_db != null) _db.Dispose();
            _db = null;
        }
    }
}
