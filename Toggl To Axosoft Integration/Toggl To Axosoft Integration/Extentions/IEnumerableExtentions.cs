using System.Collections.Generic;

namespace Toggl_To_Axosoft_Integration.Extentions
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