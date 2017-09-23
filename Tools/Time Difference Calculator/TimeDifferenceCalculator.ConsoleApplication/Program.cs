using System;
using System.Collections.Generic;
using System.Text;

namespace TimeDifferenceCalculator.ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			TimeSpan firstValue, secondValue;
			bool isEditing = false;

			Console.WriteLine("a Time calculator that works out the Time Span/Time different from one time to the other. Format (HH:mm). To exit type e or exit.");
			do
			{
				Console.WriteLine("Enter the first time.");
				var first = Console.ReadLine();
				var isExitingOnFirstValue = first != null && (first.ToLower().StartsWith("e") || first.ToLower().Contains("exit"));
				if (isExitingOnFirstValue) { break; }

				Console.WriteLine("Enter the second time.");
				var second = Console.ReadLine();
				var isExitingOnSecondValue = second != null && (second.ToLower().StartsWith("e") || second.ToLower().Contains("exit"));
				if (isExitingOnSecondValue) { break; }

				var isValidFirstValue = TimeSpan.TryParse(first, out firstValue);
				var isValidSecondValue = TimeSpan.TryParse(second, out secondValue);

				if (isValidFirstValue && isValidSecondValue)
				{
					var diff = secondValue - firstValue;
					Console.WriteLine("From {0} to {1} difference is {2}, in number: {3}", firstValue, secondValue, diff, diff.TotalHours);
				}
				isEditing = isExitingOnFirstValue || isExitingOnSecondValue;
			} while (!isEditing);
		}
	}
}
