namespace Base64StringFileConverter.API
{
	/// <summary>
	/// A class containing all 
	/// </summary>
	public static class Base64StringConverters
	{
		/// <summary>
		/// To the content.
		/// </summary>
		/// <param name="base64String">The base64 string.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">message - base64String</exception>
		public static byte[] ToContent(this string base64String)
		{
			if (string.IsNullOrWhiteSpace(base64String))
			{
				throw new System.ArgumentException("message", nameof(base64String));
			}

			return new byte[0];
		}
	}
}
