using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Password_Generator
{
	public enum CharSet
	{
		SpecialChar,
		Alpha,
		Numeric,
	}

	public enum CaseType
	{
		Mixed,
		Lower,
		Upper,
	}

	public class KeyGenerator
	{
		public static readonly char[] SpecialChar = @"!@#$%^&*_+|\=-`~".ToCharArray();
		public static readonly char[] AlphaLower = @"abcdefghijklmnopqrstuvwxyz".ToCharArray();
		public static readonly char[] AlphaUpper = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
		public static readonly char[] Numberic = @"1234567890".ToCharArray();

		private static char[] GetCharSet(CharSet[] charSets, CaseType caseTypes)
		{
			var list = new List<char>();
			foreach (var charSet in charSets)
			{
				switch (charSet)
				{
					case CharSet.SpecialChar:
						switch (caseTypes)
						{
							case CaseType.Mixed:
							case CaseType.Lower:
							case CaseType.Upper:
								list.AddRange(SpecialChar);
								break;
							default:
								break;
						}
						break;
					case CharSet.Alpha:
						switch (caseTypes)
						{
							case CaseType.Mixed:
								list.AddRange(AlphaLower);
								list.AddRange(AlphaUpper);
								break;
							case CaseType.Lower:
								list.AddRange(AlphaLower);
								break;
							case CaseType.Upper:
								list.AddRange(AlphaUpper);
								break;
							default:
								break;
						}
						break;
					case CharSet.Numeric:
						switch (caseTypes)
						{
							case CaseType.Mixed:
							case CaseType.Lower:
							case CaseType.Upper:
								list.AddRange(Numberic);
								break;
							default:
								break;
						}
						break;
					default:
						list.AddRange(AlphaLower);
						list.AddRange(AlphaUpper);
						list.AddRange(Numberic);
						break;
				}
			}
			return list.ToArray();
		}

		public static string GeneratePassword(int maxSize, CharSet[] charSets, CaseType caseType)
		{
			char[] chars = GetCharSet(charSets, caseType);
			byte[] data = new byte[1];
			{
				RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
				crypto.GetNonZeroBytes(data);
				data = new byte[maxSize];
				crypto.GetNonZeroBytes(data);
			}
			StringBuilder result = new StringBuilder(maxSize);
			foreach (byte b in data)
			{
				result.Append(chars[b % (chars.Length)]);
			}
			return result.ToString();
		}
	}
}
