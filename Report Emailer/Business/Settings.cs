using Business.Helpers;
using System;
using System.IO;

namespace Business
{
	public class Settings
	{
		// Constructors
		public Settings()
		{
			CheckAndCreateApplicationDataPath();
		}
		public Settings(Settings settings)
		{
			CheckAndCreateApplicationDataPath();
			SetProperties(settings);
		}
		public Settings(string server, string database, string username, string password)
		{
			CheckAndCreateApplicationDataPath();
			Server = server;
			Database = database;
			Username = username;
			Password = password;
		}
		public Settings(string server, string database, string username, string password, string smtphost, string @from, string to, string cc)
			: this(server, database, username, password)
		{
			SMTPHost = smtphost;
			From = @from;
			To = to;
			Cc = cc;
		}
		public Settings(string server, string database, string username, string password, string smtphost, string @from, string to,
						string cc, string emailSubject, string emailBody)
			: this(server, database, username, password, smtphost, @from, to, cc)
		{
			EmailSubject = emailSubject;
			EmailBody = emailBody;
		}

		// Properties
		public string Server { get; set; }
		public string Database { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string SMTPHost { get; set; }
		public string SMTPUserName { get; set; }
		public string SMTPPassword { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Cc { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public string Filename { get; set; }
		public string Datasets { get; set; }

		//Private Fields 
		private readonly string SettingsFileName = "settings.xml";
		private string _settingsFolderPath;
		private string SettingsFolderPath
		{
			get
			{
				return !string.IsNullOrWhiteSpace(_settingsFolderPath)
					? _settingsFolderPath
					: (_settingsFolderPath = GetApplicationDataPath());
			}
			set { _settingsFolderPath = value; }
		}
		private static string GetApplicationDataPath()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
		}

		//Public Helpers
		public Settings SetProperties(Settings settings)
		{
			Server = settings.Server;
			Database = settings.Database;
			Username = settings.Username;
			Password = settings.Password;
			SMTPHost = settings.SMTPHost;
			From = settings.From;
			To = settings.To;
			Cc = settings.Cc;
			EmailSubject = settings.EmailSubject;
			EmailBody = settings.EmailBody;
			Filename = settings.Filename;
			Datasets = settings.Datasets;
			return this;
		}
		private void CheckAndCreateApplicationDataPath()
		{
			var path = GetApplicationDataPath();
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		//Public Methods
		public Settings Get()
		{
			SetProperties(
				File.ReadAllText(
					Path.Combine(SettingsFolderPath
						, SettingsFileName))
					.ToObject<Settings>()
				);
			return this;
		}
		public Settings Save()
		{
			File.WriteAllText(
				Path.Combine(SettingsFolderPath
					, SettingsFileName)
				, this.ToXml());
			return this;
		}

	}
}