using Business.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows.Input;
using UI.Helpers;

namespace UI.ViewModel
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class MainViewModel : ViewModelBase
	{
		/*Private Fields*/
		private int _commandTimeout;
		private string _server;
		private string _database;
		private string _cc;
		private string _to;
		private string _from;
		private string _password;
		private string _username;
		private string _smtpHost;
		private string _smtpUsername;
		private string _smtpPassword;
		private string _emailSubject;
		private string _emailBody;
		private string _filename;
		private string _datasets;
		private bool _isSettingsChanged;
		private string _sql;
		private string _results;
		private string _title;
		private string _currentOpenFullFileName;
		private const string MainTitle = "Report Emailer";

		/*Construction*/
		public MainViewModel()
		{
			LoadFileTypes();
			LoadSettings();
			LoadSqlFile();
			SetTitle();
			SetCurrentOpenFile();
			_commandTimeout = 600;
		}

		/*Loading*/
		private void LoadFileTypes()
		{
			FileTypes = new List<KeyValue<FileType, string>>
			{
				new KeyValue<FileType, string>(FileType.xlsx, "Excel"),
				new KeyValue<FileType, string>(FileType.pdf, "Pdf"),
			};
		}
		private void LoadSettings()
		{
			try
			{
				var settings = new Business.Settings().Get();
				Server = settings.Server;
				Database = settings.Database;
				Username = settings.Username;
				Password = settings.Password;
				SmtpHost = settings.SMTPHost;
				SmtpUsername = settings.SMTPUserName;
				SmtpPassword = settings.SMTPPassword;
				From = settings.From;
				To = settings.To;
				Cc = settings.Cc;
				EmailSubject = settings.EmailSubject;
				EmailBody = settings.EmailBody;
				Filename = settings.Filename;
				Datasets = settings.Datasets;
				EmailBody = settings.EmailBody;
				IsSettingsChanged = false;
			}
			catch (Exception e)
			{
				WriteToResults(e.Message);
				IsSettingsChanged = false;
			}
		}
		private void LoadSqlFile()
		{
			if (Business.Config.IsCommandLineArgumentsValid)
			{
				Sql = File.ReadAllText(Business.Config.CommandLineArguments[0]);
			}
		}
		private void SetTitle()
		{
			Title = Business.Config.IsCommandLineArgumentsValid
				? string.Format("{0} - {1}", MainTitle, Business.Config.CommandLineArguments[0])
				: MainTitle;
		}
		private void SetTitle(string fileName)
		{
			Title = !string.IsNullOrWhiteSpace(fileName)
				? string.Format("{0} - {1}", MainTitle, fileName)
				: MainTitle;
		}
		private void SetCurrentOpenFile()
		{
			CurrentOpenFullFileName = Business.Config.IsCommandLineArgumentsValid
				? Business.Config.CommandLineArguments[0]
				: null;
		}
		private void SetCurrentOpenFile(string fileName)
		{
			CurrentOpenFullFileName = fileName;
		}

		/*Command Hook-ups*/
		public ICommand ViewSqlResultsCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  WindowHelpers.ShowDataGrid(
					  Business.Database.Connections.GetDataSet(
						  Sql,
						  GetSqlConnectionStringBuilder().ConnectionString,
						  CommandTimeout));
				  WriteToResults("view results successful.");
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  }, IfCanExecuteSql);
			}
		}
		public ICommand SendEmailCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  string to = null;
				  string cc = null;
				  if (!string.IsNullOrWhiteSpace(To))
					  to = To.Replace(';', ',').Replace(Environment.NewLine, ",");
				  if (!string.IsNullOrWhiteSpace(Cc))
					  cc = Cc.Replace(';', ',').Replace(Environment.NewLine, ",");
				  var zipAttachment = GetZipAttachment();
				  using (var stream = new MemoryStream(zipAttachment))
				  {
					  var message = new MailMessage(From, to)
					  {
						  Subject = EmailSubject,
						  Body = EmailBody,
						  IsBodyHtml = false,
					  };
					  if (!string.IsNullOrWhiteSpace(cc))
						  message.CC.Add(cc);
					  message.Attachments.Add(new Attachment(stream, Filename + ".zip"));
					  var smtp = new SmtpClient(SmtpHost)
					  {
						  Timeout = 600000,
						  DeliveryMethod = SmtpDeliveryMethod.Network,
					  };
					  smtp.Send(message);
					  WriteToResults("email sent.");
				  }
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  }, IfCanExecuteSql);
			}
		}
		public ICommand SaveSettingsCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  new Business.Settings
				  {
					  Server = Server,
					  Database = Database,
					  Username = Username,
					  Password = Password,
					  SMTPHost = SmtpHost,
					  SMTPUserName = SmtpUsername,
					  SMTPPassword = SmtpPassword,
					  From = From,
					  To = To,
					  Cc = Cc,
					  EmailSubject = EmailSubject,
					  EmailBody = EmailBody,
					  Filename = Filename,
					  Datasets = Datasets,
				  }.Save();
				  WriteToResults("settings saved.");
				  IsSettingsChanged = false; // Is settings still changed equals to when settings is not saved.
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
				  IsSettingsChanged = true; // Is settings still changed equals to when settings is not saved.
			  }
		  }, IfCanSaveSttings);
			}
		}
		public ICommand RevertSettingsCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  LoadSettings();
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  });
			}
		}
		public ICommand DownloadSqlResultsCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  var zipAttachment = GetZipAttachment();
				  var dialog = new Microsoft.Win32.SaveFileDialog
				  {
					  FileName = Filename + ".zip",
					  Filter = "Zip files (*.zip)|*.zip",
				  };
				  var success = dialog.ShowDialog();
				  if (success != null && success == true)
				  {
					  File.WriteAllBytes(dialog.FileName, zipAttachment);
					  WriteToResults("zip file downloaded.");
				  }
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  }, IfCanExecuteSql);
			}
		}
		public ICommand SelectDatabaseCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  var databases = Business.Database.Connections.GetDataTable(
					  @"select d.database_id, d.name from sys.databases d order by d.name",
					  GetSqlConnectionStringBuilder("master").ConnectionString, CommandTimeout)
					  .AsEnumerable()
					  .Select(i => new KeyValue<string, string>(
						  i.Field<int>("database_id").ToString(CultureInfo.InvariantCulture),
						  i.Field<string>("name")))
					  .ToList();
				  var result = WindowHelpers.ShowSelectListDialog(databases);
				  if (result.Success)
				  {
					  Database = result.Result.Value;
				  }
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  }, () => !string.IsNullOrWhiteSpace(Server));
			}
		}
		public ICommand NewFileCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  Sql = string.Empty;
				  Title = MainTitle;
				  SetCurrentOpenFile(null);
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  });
			}
		}
		public ICommand OpenFileCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  var dialog = new Microsoft.Win32.OpenFileDialog
				  {
					  Filter = "Sql files (*.sql)|*.sql",
				  };
				  var success = dialog.ShowDialog();
				  if (success != null && success == true)
				  {
					  Sql = File.ReadAllText(dialog.FileName);
					  SetTitle(dialog.FileName);
					  SetCurrentOpenFile(dialog.FileName);
					  WriteToResults("sql file opened.");
				  }
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  });
			}
		}
		public ICommand SaveFileAsCommand
		{
			get { return new RelayCommand(SaveFileAs, () => !string.IsNullOrWhiteSpace(Sql)); }
		}
		public ICommand SaveFileCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  if (!string.IsNullOrWhiteSpace(CurrentOpenFullFileName))
				  {
					  File.WriteAllText(CurrentOpenFullFileName, Sql);
					  SetTitle(CurrentOpenFullFileName);
					  WriteToResults("sql file saved.");
				  }
				  else
				  {
					  SaveFileAs();
				  }
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  }, () => !string.IsNullOrWhiteSpace(Sql));
			}
		}
		public ICommand SelectTablesCommand
		{
			get
			{
				return new RelayCommand(() =>
		  {
			  try
			  {
				  var tables = Business.Database.Connections.GetDataTable(
					  @"select t.object_id, t.name from sys.tables t order by t.name",
					  GetSqlConnectionStringBuilder().ConnectionString, CommandTimeout)
					  .AsEnumerable()
					  .Select(i => new KeyValue<string, string>(
						  i.Field<int>("object_id").ToString(CultureInfo.InvariantCulture),
						  i.Field<string>("name")))
					  .ToList();
				  var result = WindowHelpers.ShowSelectListDialog(tables);
				  if (result.Success)
				  {
					  Sql += result.Result.Value;
				  }
			  }
			  catch (Exception e)
			  {
				  WriteToResults(e.Message);
			  }
		  }, () => !string.IsNullOrWhiteSpace(Database));
			}
		}

		/*Command Checks*/
		private bool IfCanExecuteSql()
		{
			return !string.IsNullOrWhiteSpace(Server) &&
				   !string.IsNullOrWhiteSpace(Database) &&
				   !string.IsNullOrWhiteSpace(Username) &&
				   !string.IsNullOrWhiteSpace(Password) &&
				   !string.IsNullOrWhiteSpace(Sql);
		}
		private bool IfCanSaveSttings()
		{
			return IsSettingsChanged &&
				   !string.IsNullOrWhiteSpace(Server) &&
				   !string.IsNullOrWhiteSpace(Database) &&
				   !string.IsNullOrWhiteSpace(Username) &&
				   !string.IsNullOrWhiteSpace(Password);
		}

		/*Commands with 2 and more usages*/
		private void SaveFileAs()
		{
			try
			{
				var dialog = new Microsoft.Win32.SaveFileDialog
				{
					Filter = "Sql files (*.sql)|*.sql",
				};
				var success = dialog.ShowDialog();
				if (success != null && success == true)
				{
					File.WriteAllText(dialog.FileName, Sql);
					SetTitle(dialog.FileName);
					SetCurrentOpenFile(dialog.FileName);
					WriteToResults("sql file saved.");
				}
			}
			catch (Exception e)
			{
				WriteToResults(e.Message);
			}
		}

		/*Helpers*/
		private void WriteToResults(string line)
		{
			var resultLine = string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), line);
			Results = resultLine + Environment.NewLine + Results;
		}
		private byte[] GetZipAttachment()
		{
			//get data set
			var dataSet = Business.Database.Connections.GetDataSet(Sql, GetSqlConnectionStringBuilder().ConnectionString, CommandTimeout);
			//generate report file in memory based in file type selected. 
			var reportFile = new byte[0];
			switch (FileTypeSelected)
			{
				case FileType.xlsx:
					reportFile = ExcelHelpers.BuildExcelFile(dataSet, Datasets);
					break;
				case FileType.pdf:
					//reportFile =  BuildPdf(dataSet);
					break;
			}
			//insert report file to zip file in memory
			byte[] zipFile = ZipHelpers.ZipFile(reportFile, Filename, FileTypeSelected);
			//return Zip Attachment
			return zipFile;
		}
		private SqlConnectionStringBuilder GetSqlConnectionStringBuilder()
		{
			return GetSqlConnectionStringBuilder(Database);
		}
		private SqlConnectionStringBuilder GetSqlConnectionStringBuilder(string database)
		{
			return new SqlConnectionStringBuilder
			{
				DataSource = Server,
				InitialCatalog = database,
				UserID = Username,
				Password = Password,
				ConnectTimeout = CommandTimeout,
			};
		}

		/*Properties*/
		public string Results
		{
			get { return _results; }
			set
			{
				_results = value;
				RaisePropertyChanged(() => Results);
			}
		}
		public string Sql
		{
			get { return _sql; }
			set
			{
				_sql = value;
				RaisePropertyChanged(() => Sql);
			}
		}
		public bool IsSettingsChanged
		{
			get { return _isSettingsChanged; }
			set
			{
				_isSettingsChanged = value;
				RaisePropertyChanged(() => IsSettingsChanged);
			}
		}

		public string Server
		{
			get { return _server; }
			set
			{
				if (_server == value) return;
				_server = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => Server);
			}
		}
		public int CommandTimeout
		{
			get { return _commandTimeout; }
			set
			{
				if (_commandTimeout == value) return;
				_commandTimeout = value;
				//IsSettingsChanged = true;
				RaisePropertyChanged(() => CommandTimeout);
			}
		}
		public string Database
		{
			get { return _database; }
			set
			{
				if (_database == value) return;
				_database = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => Database);
			}
		}
		public string Username
		{
			get { return _username; }
			set
			{
				if (_username == value) return;
				_username = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => Username);
			}
		}
		public string Password
		{
			get { return _password; }
			set
			{
				if (_password == value) return;
				_password = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => Password);
			}
		}
		public string SmtpHost
		{
			get { return _smtpHost; }
			set
			{
				if (_smtpHost == value) return;
				_smtpHost = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => SmtpHost);
			}
		}
		public string SmtpUsername
		{
			get { return _smtpUsername; }
			set
			{
				if (_smtpUsername == value) return;
				_smtpUsername = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => SmtpUsername);
			}
		}
		public string SmtpPassword
		{
			get { return _smtpPassword; }
			set
			{
				if (_smtpPassword == value) return;
				_smtpPassword = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => SmtpPassword);
			}
		}
		public string From
		{
			get { return _from; }
			set
			{
				if (_from == value) return;
				_from = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => From);
			}
		}
		public string To
		{
			get { return _to; }
			set
			{
				if (_to == value) return;
				_to = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => To);
			}
		}
		public string Cc
		{
			get { return _cc; }
			set
			{
				if (_cc == value) return;
				_cc = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => Cc);
			}
		}
		public string EmailSubject
		{
			get { return _emailSubject; }
			set
			{
				if (_emailSubject == value) return;
				_emailSubject = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => EmailSubject);
			}
		}
		public string EmailBody
		{
			get { return _emailBody; }
			set
			{
				if (_emailBody == value) return;
				_emailBody = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => EmailBody);
			}
		}
		public string Filename
		{
			get { return _filename; }
			set
			{
				if (_filename == value) return;
				_filename = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => Filename);
			}
		}
		public string Datasets
		{
			get { return _datasets; }
			set
			{
				if (_datasets == value) return;
				_datasets = value;
				IsSettingsChanged = true;
				RaisePropertyChanged(() => Datasets);
			}
		}

		public List<KeyValue<FileType, string>> FileTypes { get; set; }
		public FileType FileTypeSelected { get; set; }
		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				RaisePropertyChanged(() => Title);
			}
		}

		public string CurrentOpenFullFileName
		{
			get { return _currentOpenFullFileName; }
			set
			{
				_currentOpenFullFileName = value;
				RaisePropertyChanged(() => CurrentOpenFullFileName);
			}
		}

	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}