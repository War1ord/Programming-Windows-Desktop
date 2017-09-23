using Business_Management.Business.Config;
using Business_Management.Business.Extentions;
using Business_Management.Business.Models;
using Business_Management.Business.Models.FirstTimeSetup;
using Business_Management.Business.Setup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace Business_Management.UserInterface.Admin.ViewModels.Setup
{
	public class ConnectionSetupViewModel : ViewModelBase
	{
		public ConnectionSetupViewModel()
		{
			LocalConnection = true;
			WindowsAuthentication = true;
		}

		private bool localConnection;
		private bool remoteConnection;
		private bool windowsAuthentication;
		private bool sqlServerAuthentication;
		private string server;
		private string sqlInstance;
		private string username;
		private SecureString password;

		public bool IsEnabledServer
		{
			get
			{
				return remoteConnection;
			}
		}
		public bool IsEnabledSqlInstance
		{
			get
			{
				return true;
			}
		}
		public bool IsEnabledUsername
		{
			get
			{
				return sqlServerAuthentication;
			}
		}
		public bool IsEnabledSecurePassword
		{
			get
			{
				return sqlServerAuthentication;
			}
		}

		public string Server
		{
			get
			{
				return server;
			}
			set
			{
				server = value;
				RaisePropertyChanged(() => SaveCommand);
			}
		}
		public string SqlInstance
		{
			get
			{
				return sqlInstance;
			}
			set
			{
				sqlInstance = value;
			}
		}
		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value;
				RaisePropertyChanged(() => SaveCommand);
			}
		}

		public SecureString SecurePassword
		{
			private get
			{
				return password;
			}
			set
			{
				password = value;
				RaisePropertyChanged(() => SaveCommand);
			}
		}

		public bool LocalConnection
		{
			get
			{
				return localConnection;
			}
			set
			{
				localConnection = value;
				RaisePropertyChanged(() => SaveCommand);
			}
		}
		public bool RemoteConnection
		{
			get
			{
				return remoteConnection;
			}
			set
			{
				remoteConnection = value;
				RaisePropertyChanged(() => IsEnabledServer);
				RaisePropertyChanged(() => SaveCommand);
			}
		}
		public bool WindowsAuthentication
		{
			get
			{
				return windowsAuthentication;
			}
			set
			{
				windowsAuthentication = value;
				RaisePropertyChanged(() => SaveCommand);
			}
		}
		public bool SqlServerAuthentication
		{
			get
			{
				return sqlServerAuthentication;
			}
			set
			{
				sqlServerAuthentication = value;
				RaisePropertyChanged(() => IsEnabledUsername);
				RaisePropertyChanged(() => IsEnabledSecurePassword);
				RaisePropertyChanged(() => SaveCommand);
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand<Window>(
					canExecute: (w) => { return IsSaveCommandEnabled; },
					execute: SaveConnection);
			}
		}
		public ICommand CancelCommand { get { return new RelayCommand<Window>(execute: (w) => w.Close()); } }

		private bool IsSaveCommandEnabled
		{
			get
			{
				if (LocalConnection && WindowsAuthentication)
				{
					return true;
				}
				else if (LocalConnection && SqlServerAuthentication)
				{
					return Username.IsSet()
						&& SecurePassword.IsSet()
						&& SecurePassword.Length > 0;
				}
				else if (RemoteConnection && WindowsAuthentication)
				{
					return Server.IsSet();
				}
				else if (RemoteConnection && SqlServerAuthentication)
				{
					return Server.IsSet()
						&& Username.IsSet()
						&& SecurePassword.IsSet()
						&& SecurePassword.Length > 0;
				}
				else
				{
					return false;
				}
			}
		}
		private void SaveConnection(Window window)
		{
			//TODO: UI message is not displaying, changed to MessageBox but needs to be UI element that update while command is excuting.
			MessageBox.Show(
				owner: window,
				messageBoxText: @"Saving ...",
				caption: @"Saving",
				button: MessageBoxButton.OK,
				icon: MessageBoxImage.Asterisk);
			var assemblyLocation = GetType().Assembly.Location;
			//set DefaultConnection with built connection setting
			Result result_connectionString;
			using (var db = ConnectionSetup.Instance)
			{
				result_connectionString = db.SaveConnectionSetup(new ConnectionSetupData
				{
					LocalConnection = LocalConnection,
					RemoteConnection = RemoteConnection,
					WindowsAuthentication = WindowsAuthentication,
					SqlServerAuthentication = SqlServerAuthentication,
					Server = Server,
					SqlInstance = SqlInstance,
					Username = Username,
					SecurePassword = SecurePassword,
					AssemblyLocation = assemblyLocation,
				});
			}
			if (result_connectionString.IsError)
			{
				var UIMessage = result_connectionString.Message + (result_connectionString.Exception.IsSet() ? " - Message: " + result_connectionString.Exception.Message : "");
				MessageBox.Show(
					messageBoxText: UIMessage,
					caption: @"Error",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Error);
				return;
			}
			//Initialize Database
			var result_database = Business.Data.DataContext.InitializeDatabase();
			if (result_database.IsError)
			{
				var UIMessage = result_database.Message + (result_database.Exception.IsSet() ? " - Message: " + result_database.Exception.Message : "");
				MessageBox.Show(
					messageBoxText: UIMessage,
					caption: @"Error",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Error);
				return;
			}
			//set AppSettings ConnectionSetupNeeded to false
			var result_appSetting = EditConfigFile.UpdateAppSettings(assemblyLocation,
				new KeyValuePair<string, string>("ConnectionSetupNeeded", "false"));
			if (result_appSetting.IsError)
			{
				var UIMessage = result_appSetting.Message + (result_appSetting.Exception.IsSet() ? " - Message: " + result_appSetting.Exception.Message : "");
				MessageBox.Show(
					messageBoxText: UIMessage,
					caption: @"Error",
					button: MessageBoxButton.OK,
					icon: MessageBoxImage.Error);
				return;
			}
			//show success message
			MessageBox.Show(
				owner: window,
				messageBoxText: @"The Connection settings was saved successfully",
				caption: @"success",
				button: MessageBoxButton.OK,
				icon: MessageBoxImage.Asterisk);
			//close window
			if (window.IsSet()) window.Close();
		}
	}
}
