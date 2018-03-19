using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Linq;
using System.Windows.Input;
using Toggl_Reports.Models;
using Toggl_Reports.Services;

namespace Toggl_Reports.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string apiKey;
        private DateTime fromDate;
        private DateTime toDate;
        private DateCollection[] entries;
        private readonly TogglTimeEntriesService service;
        private readonly MainWindow owner;

        public MainViewModel(TogglTimeEntriesService service, MainWindow owner)
        {
            this.owner = owner;
            this.service = service;
            this.FromDate = DateTime.Today.AddDays(-1);
            this.ToDate = DateTime.Today.Add(new TimeSpan(23, 59, 59));
            this.entries = new DateCollection[0];
            SearchEntries();
        }

        public DateCollection[] Entries
        {
            get { return this.entries; }
            set
            {
                this.entries = value;
                RaisePropertyChanged(() => this.Entries);
            }
        }
        public string ApiKey
        {
            get { return this.apiKey; }
            set
            {
                this.service.ApiKey = this.apiKey = value;
                RaisePropertyChanged(() => this.ApiKey);
            }
        }
        public DateTime FromDate
        {
            get { return this.fromDate.Date; }
            set
            {
                this.fromDate = value;
                RaisePropertyChanged(() => this.FromDate);
            }
        }
        public DateTime ToDate
        {
            get { return this.toDate.Date.Add(new TimeSpan(23, 59, 59)); }
            set
            {
                this.toDate = value;
                RaisePropertyChanged(() => this.ToDate);
            }
        }
        public DateRange DateRange { get { return new DateRange(this.FromDate, this.ToDate); } }

        public ICommand Search
        {
            get
            {
                return new RelayCommand(
                    canExecute: () => this.FromDate != DateTime.MinValue && this.ToDate != DateTime.MinValue && this.fromDate <= this.toDate,
                    execute: () => this.SearchEntries()
                );
            }
        }
        public ICommand ExportToExcel
        {
            get
            {
                return new RelayCommand(
                    canExecute: () => this.Entries != null && this.Entries.Any(),
                    execute: () =>
                    {
                        //var dialog = new Microsoft.Win32.SaveFileDialog
                        //{
                        //    Filter = "Excel file|*.xlsx"
                        //};
                        //if (dialog.ShowDialog() == true)
                        //{
                        //    return;
                        //}
                        //else
                        //{
                        //    return;
                        //}
                    });
            }
        }

        private void SearchEntries()
        {
            if (!string.IsNullOrWhiteSpace(this.apiKey))
            {
                this.Entries = this.service.GetTogglCalculatedEntriesByDate(this.DateRange);
            }
        }

    }
}