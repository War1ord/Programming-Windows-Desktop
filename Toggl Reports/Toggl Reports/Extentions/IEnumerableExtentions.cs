using System.Collections.Generic;

namespace Toggl_Reports.Extentions
{
    public static class EnumerableExtentions
    {
        public static string FirstNumber(this IEnumerable<string> list)
        {
            if (list == null)
                return null;
            foreach (var text in list)
            {
                int number;
                var isNumber = int.TryParse(text, out number);
                if (isNumber)
                {
                    return text;
                }
            }
            return null;
        }
    }
}