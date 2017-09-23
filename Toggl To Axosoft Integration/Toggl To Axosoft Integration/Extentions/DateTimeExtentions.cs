using System;
using System.Collections.Generic;

namespace Toggl_To_Axosoft_Integration.Extentions
{
    public static class DateTimeExtentions
    {
        public static IEnumerable<DateTime> GetDaysTo(this DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        } 
    }
}