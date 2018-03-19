using System;

namespace Toggl_Reports.Models
{
    public class DateCollection
    {
        public DateTime Date { get; set; }
        public CalculatedTimeEntry[] TimeEntries { get; set; }
    }
}