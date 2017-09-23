using System;
using System.Collections.Generic;
using System.Linq;

namespace Address_Book.Helpers
{
    public static class EnumHelpers
    {
        public static List<KeyValuePair<int, string>> ConvertEnumToPairedList<TEnum>() where TEnum : struct
        {
            var type = typeof(TEnum);
            if (type.IsEnum)
            {
                var list = Enum.GetValues(type)
                       .Cast<TEnum>()
                       .Select(i =>
                       {
                           string text = i.ToString();
                           Enum data = Enum.Parse(type, text) as Enum;
                           int key = Convert.ToInt32(data);
                           return new KeyValuePair<int, string>(key, text);
                       })
                       .ToList();
                return list;
            }
            else
            {
                return new List<KeyValuePair<int, string>>();
            }
        }
        public static List<TEnum> ConvertEnumToList<TEnum>() where TEnum : struct
        {
            var type = typeof(TEnum);
            if (type.IsEnum)
            {
                var list = Enum.GetValues(type)
                       .Cast<TEnum>()
                       .ToList();
                return list;
            }
            else
            {
                return new List<TEnum>();
            }
        }
    }
}