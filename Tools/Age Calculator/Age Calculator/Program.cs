using DateTimeExtensions.NaturalText;
using System;

namespace Age_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var exit = true;
            do
            {
                Console.WriteLine(@"Enter a date and time to calculate the age from, ");
                Console.WriteLine(@"with format as (year-month-day hour:minute:second)? ");
                DateTime datetime;
                var valid = DateTime.TryParse(Console.ReadLine(), out datetime);
                Console.WriteLine(valid
                    ? Build_Age_Display(datetime)
                    : @"Date and time format is invalid.");
                Console.WriteLine(@"Do you want to exit? ");
                var isMore = bool.TryParse(ParseYesNoToBool(Console.ReadLine()), out exit);
                Console.WriteLine();
            } while (!exit);
        }

        private static string Build_Age_Display(DateTime input_date)
        {
            var age = new DateDiff(input_date, DateTime.Now);
            return string.Format("Years:{0} Months:{1} Days:{2} Hours:{3} Minutes:{4} Seconds:{5} "
                , age.Years, age.Months, age.Days, age.Hours, age.Minutes, age.Seconds);
        }

        private static string ParseYesNoToBool(string input_value)
        {
            const string true_value = "true";
            const string false_value = "false";
            if (!string.IsNullOrEmpty(input_value))
            {
                switch (input_value.Trim().ToLowerInvariant())
                {
                    case "y":
                    case "yes":
                        return true_value;
                    case "n":
                    case "no":
                        return false_value;
                    default:
                        return true_value;
                }
            }
            else
            {
                return true_value;
            }
        }
    }
}
