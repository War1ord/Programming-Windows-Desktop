using CefSharp;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Toggl_To_Axosoft_Integration.Business;
using Toggl_To_Axosoft_Integration.Business.Api.Toggl;
using Toggl_To_Axosoft_Integration.Extentions;

namespace Toggl_To_Axosoft_Integration.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private DateTime _fromDate;
		private DateTime _toDate;
		private Models.ColumnConfig _columns;
		private List<Models.Toggl.TimeEntryProjectCollection> _entries;
		private string _address;
		private string _html;
		private const string LocalUrl = "http://local/";

		public MainViewModel(IWebBrowser browser)
		{
			Browser = browser;
			FromDate = DateTime.Today.AddDays(-1);
			ToDate = DateTime.Today.Add(new TimeSpan(23, 59, 59));
			SearchEntries();
		}

		public IWebBrowser Browser { get; private set; }

		public Models.ColumnConfig Columns
		{
			get
			{
				var config = new Models.ColumnConfig { Columns = new List<Models.Column>() };
				//config.Columns.Add(new Models.Column {Header = "Project", DataField = "Key.ProjectId"});
				//config.Columns.Add(new Models.Column {Header = "Ticket", DataField = "Key.Ticket"});

				foreach (var date in FromDate.Date.GetDaysTo(ToDate.Date))
				{
					var header = date.ToString("yyyy-MM-dd");
					config.Columns.Add(new Models.Column { Header = header, DataField = header });
				}

				return config;
			}
		}

		public List<Models.Toggl.TimeEntryProjectCollection> Entries
		{
			get { return _entries; }
			set
			{
				_entries = value;
				RaisePropertyChanged(() => Entries);
			}
		}
		public DateTime FromDate
		{
			get { return _fromDate.Date; }
			set
			{
				_fromDate = value;
				RaisePropertyChanged(() => FromDate);
			}
		}
		public DateTime ToDate
		{
			get { return _toDate.Date.Add(new TimeSpan(23, 59, 59)); }
			set
			{
				_toDate = value;
				RaisePropertyChanged(() => ToDate);
			}
		}
		public Models.DateRange DateRange { get { return new Models.DateRange(FromDate, ToDate); } }
		public string Address
		{
			get { return _address ?? (_address = LocalUrl); }
			set
			{
				_address = value;
				RaisePropertyChanged(() => Address);
			}
		}
		public string Html
		{
			get { return _html ?? (_html = ""); }
			set
			{
				_html = value;
				RaisePropertyChanged(() => Html);
				LoadHtml();
			}
		}

		public ICommand Search
		{
			get
			{
				return new RelayCommand(
					canExecute: () => FromDate != DateTime.MinValue &&
										ToDate != DateTime.MinValue,
					execute: SearchEntries);
			}
		}
		public ICommand ExportToExcel
		{
			get
			{
				return new RelayCommand(
					canExecute: () => Entries != null && Entries.Any(),
					execute: () => ExecuteExportToExcel());
			}
		}

		public ICommand LogToAxosoft
		{
			get
			{
				return new RelayCommand(
					canExecute: () => false,
					execute: () =>
					{
						return;
					});
			}
		}

		public void LoadHtml()
		{
			Browser.LoadHtml(Html, LocalUrl);
			Address = LocalUrl;
		}

		private void SearchEntries()
		{
			RaisePropertyChanged(() => Columns);
			Entries = TogglTimeEntriesService.GetTogglCalculatedDateEntries(DateRange);
			Html = TemplateEngine.Instance.RunAndCompileIfNeeded_Report(this);
		}

		private static void ExecuteExportToExcel()
		{
			var dialog = new Microsoft.Win32.SaveFileDialog
			{
				Filter = "Excel file|*.xlsx"
			};
			if (dialog.ShowDialog() == true)
			{

				return;
			}
			else
			{

				return;
			}
		}

	}
}