using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Extentions
{
	public static class DbEntityValidationResultExtentions
	{
		public static string ToMultilineString(this DbEntityValidationResult result)
		{
			return result.ValidationErrors.Select(i => i.ErrorMessage).Concatenate();
		}

		public static string ToMultilineString(this List<KeyValuePair<string, DbEntityValidationResult>> results)
		{
            StringBuilder s = new StringBuilder();
			foreach (var result in results)
			{
			    s.AppendLine(result.Key);
			    s.AppendLine(result.Value.ToMultilineString());
			}
			return s.ToString();
		}
	}
}
