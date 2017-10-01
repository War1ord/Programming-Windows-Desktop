using System;

namespace Base64StringFileConverter.API
{
	/// <summary>
	/// a Class containing all extension methods in the api.
	/// </summary>
	public static class Base64StringConverters
	{
		/// <summary>
		/// Converts the base 64 string to content as a byte array.
		/// </summary>
		/// <param name="base64String">The base64 string.</param>
		/// <returns>
		/// The Content of the file as a byte array
		/// </returns>
		/// <exception cref="ArgumentNullException">base64String</exception>
		public static byte[] ToContent(this string base64String)
		{
			if (string.IsNullOrWhiteSpace(base64String))
				throw new ArgumentNullException(nameof(base64String));
			else
				return Convert.FromBase64String(base64String);
		}

		/// <summary>
		/// Converts the byte array to content as a base 64 string.
		/// </summary>
		/// <param name="fileContent">Content of the file.</param>
		/// <returns>
		/// The Content of the file as a base 64 string.
		/// </returns>
		/// <exception cref="ArgumentNullException">fileContent</exception>
		public static string ToBase64String(this byte[] fileContent)
		{
			if (fileContent == null)
				throw new ArgumentNullException(nameof(fileContent));
			else
				return Convert.ToBase64String(fileContent);
		}

	}
}
