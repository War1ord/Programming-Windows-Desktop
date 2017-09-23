using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;

namespace Framework.Wpf.Mvvm.UI.Helpers.Extentions
{
	public static class ValidationErrorsResultsExtention
	{
		public static string ToErrorString(this IEnumerable<DbValidationError> validationErrors)
		{
			StringBuilder result = new StringBuilder();
			foreach (var validationError in validationErrors)
			{
				result.Append(string.Format("{0}: {1}\r\n", validationError.PropertyName, validationError.ErrorMessage));
			}
			return result.ToString();
		}
	}
}