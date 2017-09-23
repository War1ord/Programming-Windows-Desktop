using System.Security;

namespace Business_Management.Business.Models.FirstTimeSetup
{
	public class ConnectionSetupData : BusinessModelBase
	{
		public bool LocalConnection { get; set; }
		public bool RemoteConnection { get; set; }
		public bool WindowsAuthentication { get; set; }
		public bool SqlServerAuthentication { get; set; }
		public string Server { get; set; }
		public string SqlInstance { get; set; }
		public string Username { get; set; }
		public SecureString SecurePassword { get; set; }
		public string AssemblyLocation { get; set; }
	}
}
