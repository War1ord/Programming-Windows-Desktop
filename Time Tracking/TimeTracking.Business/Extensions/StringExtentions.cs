using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracking.Business.Extensions
{
	public static class StringExtentions
	{
		public static string Concatenate(this IEnumerable<string> source)
		{
			return string.Join(Environment.NewLine, source);
		}

		public static string Concatenate(this IEnumerable<string> source, string separator)
		{
			return string.Join(separator, source);
		}

		public static string RemoveSpaces(this string source)
		{
			return source.Trim().Replace(@" ", "");
		}

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

		public static T To<T>(this string source)
		{
			Type type = typeof(T);
			Type underlyingType = Nullable.GetUnderlyingType(type);
			bool isEmpty = string.IsNullOrWhiteSpace(source);
			bool hasUnderlyingType = underlyingType != null;
			bool isEnum = hasUnderlyingType ? underlyingType.IsEnum : type.IsEnum;
			if (isEmpty)
			{
				if (hasUnderlyingType)
				{
					return default(T);
				}
				else
				{
					throw new InvalidCastException(string.Format(
						"Can not cast to type:{0}. This is not a nullable type.", type.Name));
				}
			}
			try
			{
				if (isEnum)
				{
					return hasUnderlyingType
						? (T)Enum.Parse(underlyingType, source)
						: (T)Enum.Parse(type, source);
				}
				else
				{
					return hasUnderlyingType
						? (T)Convert.ChangeType(source, underlyingType)
						: (T)Convert.ChangeType(source, type);
				}
			}
			catch
			{
				if (hasUnderlyingType)
				{
					return default(T);
				}
				else
				{
					throw;
				}
			}
		}

		public static bool TryTo<T>(this string source, out T value)
		{
			Type type = typeof(T);
			Type underlyingType = Nullable.GetUnderlyingType(type);
			bool isEmpty = string.IsNullOrWhiteSpace(source);
			bool hasUnderlyingType = underlyingType != null;
			bool isEnum = hasUnderlyingType ? underlyingType.IsEnum : type.IsEnum;
			if (isEmpty)
			{
				if (hasUnderlyingType)
				{
					value = default(T);
					return true;
				}
				else
				{
					value = default(T);
					return false;
				}
			}
			try
			{
				if (isEnum)
				{
					value = hasUnderlyingType
						? (T)Enum.Parse(underlyingType, source)
						: (T)Enum.Parse(type, source);
				}
				else
				{
					value = hasUnderlyingType
						? (T)Convert.ChangeType(source, underlyingType)
						: (T)Convert.ChangeType(source, type);
				}
				return true;
			}
			catch
			{
				value = default(T);
				return false;
			}
		}

	}
}