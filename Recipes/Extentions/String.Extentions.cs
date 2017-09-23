using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extentions
{
	public static class StringExtentions
	{
		/// <summary>
		/// Concatenates the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static string Concatenate(this IEnumerable<string> source)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in source)
			{
				stringBuilder.Append(value + "\r\n");
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Concatenates the specified source list/Enumerable appending a separator string.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="separator">The separator.</param>
		/// <returns></returns>
		public static string Concatenate(this IEnumerable<string> source, string separator)
		{
			return string.Join(separator, source);
		}

		/// <summary>
		/// Removes the spaces from source string.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static string RemoveSpaces(this string source)
		{
			return source.Trim().Replace(@" ", "");
		}

		/// <summary>
		/// Removes the blank char's from source string.
		/// </summary>
		/// <param name="source">The string.</param>
		/// <returns></returns>
		public static string RemoveBlanks(this string source)
		{
			StringBuilder sb = new StringBuilder(source.Length);
			for (int i = 0; i < source.Length; i++)
			{
				char c = source[i];
				switch (c)
				{
					case '\r':
					case '\n':
					case '\t':
					case ' ':
						continue;
					default:
						sb.Append(c);
						break;
				}
			}
			return sb.ToString();
		}
	}
}
