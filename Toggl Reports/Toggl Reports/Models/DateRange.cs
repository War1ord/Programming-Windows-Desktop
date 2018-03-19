using System;

namespace Toggl_Reports.Models
{
    public struct DateRange
    {
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;

        public DateRange(DateTime fromDate, DateTime toDate)
        {
            this._fromDate = fromDate;
            this._toDate = toDate;
        }

        public DateTime FromDate
        {
            get { return this._fromDate; }
        }

        public DateTime ToDate
        {
            get { return this._toDate; }
        }
    }

}