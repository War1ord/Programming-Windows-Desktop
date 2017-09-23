using System;
using System.DirectoryServices;
using System.Configuration;

namespace RecycleAppPool
{
	internal class Program
	{
		private static readonly AppSettingsReader AppSettingsReader = new AppSettingsReader();

		private static readonly string ServerName = (string) AppSettingsReader.GetValue("serverName", typeof (string));

		private static readonly string AppPoolName = (string) AppSettingsReader.GetValue("appPoolName", typeof (string));

		//static readonly string Username = (string)AppSettingsReader.GetValue("Username", typeof(string));
		//static readonly string Password = (string)AppSettingsReader.GetValue("Password", typeof(string));
		/// <summary>
		/// Main entry point for the application.
		/// </summary> 
		/// <param name="args">The args.</param>
		private static void Main(string[] args)
		{
			Console.WriteLine(
				RecycleAppPool(ServerName, AppPoolName)
					? "App Pool Recycled...."
					: "Failed....");
			//Console.ReadKey();
		}

		/// <summary>
		/// Recycles the app pool.
		/// http://weblogs.asp.net/robinkedia/archive/2009/04/28/playing-around-with-iis-application-pool-using-c.aspx
		/// or %windir%\system32\inetsrv\appcmd.exe recycle apppool "my-app-pool-name"
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="adminUsername">The admin username.</param>
		/// <param name="adminPassword">The admin password.</param>
		/// <param name="appPoolName">Name of the app pool.</param>
		/// <returns></returns>
		public static bool RecycleAppPool(string serverName, string appPoolName)
		{
			DirectoryEntry appPools =
				new DirectoryEntry("IIS://" + serverName + "/w3svc/apppools" /*,Username,Password*/);
			bool status = false;
			foreach (DirectoryEntry appPool in appPools.Children)
			{
				if (appPoolName.Equals(appPool.Name, StringComparison.OrdinalIgnoreCase))
				{
					appPool.Invoke("Recycle", null);
					status = true;
					break;
				}
			}
			appPools = null;
			return status;
		}
	}
}