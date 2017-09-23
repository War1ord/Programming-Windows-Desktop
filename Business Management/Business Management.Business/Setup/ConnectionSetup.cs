using Business_Management.Business.Config;
using Business_Management.Business.Extentions;
using Business_Management.Business.Models;
using Business_Management.Business.Models.FirstTimeSetup;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;

namespace Business_Management.Business.Setup
{
	public class ConnectionSetup : Base.BusinessBase
	{
		public static ConnectionSetup Instance { get { return new ConnectionSetup(); } }

		public bool CheckIfDatabaseExists()
		{
			return Db.Database.Exists();
		}

		public bool CheckIfConnectionSetupIsNeeded()
		{
			return ConfigurationManager.AppSettings.Get("ConnectionSetupNeeded").AsBool();
		}

		public Result SaveConnectionSetup(ConnectionSetupData data)
		{
			// save ConnectionSetupData
			// step 1 : build connection string
			string connectionString = BuildConnectionString(data);
			// step 2 : call EditConfigFile to edit config file 
			return EditConfigFile.UpdateConnectionString(data.AssemblyLocation, connectionString);
		}

		private static string BuildConnectionString(ConnectionSetupData data)
		{
			const string databaseName = "BusinessManagement";
			if (data.IsSet())
			{
				if (data.LocalConnection && data.WindowsAuthentication)
				{
					return new SqlConnectionStringBuilder
					{
						InitialCatalog = databaseName,
						IntegratedSecurity = true,
						DataSource = string.Format(@".{0}{1}", data.SqlInstance.IsSet() ? @"\" : "", data.SqlInstance.DefaultTo("")),
					}.ConnectionString;
				}
				else if (data.LocalConnection && data.SqlServerAuthentication)
				{
					return new SqlConnectionStringBuilder
					{
						InitialCatalog = databaseName,
						DataSource = string.Format(@".{0}{1}", data.SqlInstance.IsSet() ? @"\" : "", data.SqlInstance.DefaultTo("")),
						UserID = data.Username,
						Password = new NetworkCredential(string.Empty, data.SecurePassword).Password,
					}.ConnectionString;
				}
				else if (data.RemoteConnection && data.WindowsAuthentication)
				{
					return new SqlConnectionStringBuilder
					{
						InitialCatalog = databaseName,
						DataSource = string.Format(@"{0}{1}{2}", data.Server, data.SqlInstance.IsSet() ? @"\" : "", data.SqlInstance.DefaultTo("")),
					}.ConnectionString;
				}
				else if (data.RemoteConnection && data.SqlServerAuthentication)
				{
					return new SqlConnectionStringBuilder
					{
						InitialCatalog = databaseName,
						DataSource = string.Format(@"{0}{1}{2}", data.Server, data.SqlInstance.IsSet() ? @"\" : "", data.SqlInstance.DefaultTo("")),
						UserID = data.Username,
						Password = new NetworkCredential(string.Empty, data.SecurePassword).Password,
					}.ConnectionString;
				}
				else
				{
					//TODO: find other ways to get connection string if possible
					return null;
				}
			}
			return null;
		}

	}
}
