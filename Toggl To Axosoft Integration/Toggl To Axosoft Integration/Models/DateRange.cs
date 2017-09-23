using System;

namespace Toggl_To_Axosoft_Integration.Models
{
    public struct DateRange
    {
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;

        public DateRange(DateTime fromDate, DateTime toDate)
        {
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public DateTime FromDate
        {
            get { return _fromDate; }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
        }
    }

}