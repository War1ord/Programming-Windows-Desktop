using System.Web;

namespace Common
{
	/// <summary>
	/// Environment object
	/// </summary>
	public static class Environment
	{
		/// <summary>
		/// Environment name (host name)
		/// </summary>
		/// <param name="context">The context.</param>
		public static string HostName(HttpContext context = null)
		{
			string hostName = string.Empty;
			try
			{
				hostName = context != null ? context.Server.MachineName : System.Environment.MachineName;
			}
			catch
			{
				/* ignore */
			}
			return hostName;
		}

		/// <summary>
		/// Environment version (host version)
		/// </summary>
		public static string HostVersion()
		{
			string version = string.Empty;
			try
			{
				version = System.Environment.Version.ToString();
			}
			catch
			{
				/* ignore */
			}
			return version;
		}
	}
}