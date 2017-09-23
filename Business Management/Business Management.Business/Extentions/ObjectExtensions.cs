using System;
using System.ComponentModel;

namespace Business_Management.Business.Extentions
{
	public static class ObjectExtensions
	{
		public static bool IsSet(this object value)
		{
			return value != null;
		}
		public static bool IsNotSet(this object value)
		{
			return value == null;
		}

		public static int AsInt(this object value)
		{
			return AsInt(value, 0);
		}

		public static int AsInt(this object value, int defaultValue)
		{
			try
			{
				return (int)value;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static decimal AsDecimal(this object value)
		{
			return AsDecimal(value, default(decimal));
		}

		public static decimal AsDecimal(this object value, decimal defaultValue)
		{
			try
			{
				return (decimal)value;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static float AsFloat(this object value)
		{
			return AsFloat(value, 0.0f);
		}

		public static float AsFloat(this object value, float defaultValue)
		{
			try
			{
				return (float)value;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static DateTime AsDateTime(this object value)
		{
			return AsDateTime(value, new DateTime());
		}

		public static DateTime AsDateTime(this object value, DateTime defaultValue)
		{
			try
			{
				return (DateTime)value;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static bool AsBool(this object value)
		{
			return AsBool(value, false);
		}

		public static bool AsBool(this object value, bool defaultValue)
		{
			try
			{
				return (bool)value;
			}
			catch
			{
				return defaultValue;
			}
		}

		public static TValue As<TValue>(this object value)
		{
			return As(value, default(TValue));
		}

		public static TValue As<TValue>(this object value, TValue defaultValue)
		{
			try
			{
				var sourceType = value.GetType();
				TypeConverter converter1 = TypeDescriptor.GetConverter(typeof(TValue));
				if (converter1.CanConvertFrom(sourceType))
					return (TValue)converter1.ConvertFrom((object)value);
				TypeConverter converter2 = TypeDescriptor.GetConverter(sourceType);
				if (converter2.CanConvertTo(typeof(TValue)))
					return (TValue)converter2.ConvertTo((object)value, typeof(TValue));
			}
			catch { }
			return defaultValue;
		}

	}
}