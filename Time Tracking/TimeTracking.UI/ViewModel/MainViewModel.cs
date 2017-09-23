using System;
using System.Runtime.InteropServices;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using TimeTracking.Models;
using TimeTracking.UI.Model;
using TimeTracking.UI.Views;

namespace TimeTracking.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _statusMessage;
        private string _startEndCommandText = "Start";
        private bool _isStarted = false;
        private WorkItem _workItem;

        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                RaisePropertyChanged(() => StatusMessage);
            }
        }
        public string StartEndCommandText
        {
            get
            {
                return _startEndCommandText;
            }
            set
            {
                _startEndCommandText = value;
                RaisePropertyChanged(() => StartEndCommandText);
            }
        }
        public WorkItem WorkItem
        {
            get
            {
                return _workItem ?? (_workItem = new WorkItem());
            }
            set
            {
                _workItem = value;
                RaisePropertyChanged(() => WorkItem);
            }
        }

        public bool IsValidDescription
        {
            get
            {
                return !string.IsNullOrWhiteSpace(WorkItem.Description);
            }
        }
        public bool IsValidClientOrProject
        {
            get
            {
                return WorkItem.ClientOrProjectGuid != Guid.Empty;
            }
        }

        public ICommand StartEndCommand
        {
            get
            {
                return new RelayCommand(() =>
                    {
                        if (IsValidDescription && IsValidClientOrProject)
                        {
                            if (!_isStarted)
                            {
                                var result = new Business.WorkItems().Start(_workItem);
                                if (result.IsSuccessful)
                                {
                                    _isStarted = true;
                                    StartEndCommandText = "End";
                                    StatusMessage = "Started";
                                }
                                else
                                {
                                    _isStarted = false;
                                    _workItem = null;
                                    StatusMessage = result.Message;
                                }
                            }
                            else
                            {
                                var result = new Business.WorkItems().End(_workItem);
                                if (result.IsSuccessful)
                                {
                                    _isStarted = false;
                                    _workItem = null;
                                    Reset();
                                    StartEndCommandText = "Start";
                                    StatusMessage = "Ended";
                                }
                                else
                                {
                                    _isStarted = true;
                                    StatusMessage = result.Message;
                                }
                            }
                        }
                        else
                        {
                            if (!IsValidDescription)
                            {
                                StatusMessage = "Please enter a description.";
                                return;
                            }
                            if (!IsValidClientOrProject)
                            {
                                StatusMessage = "Please enter a client or project.";
                                return;
                            }
                        }
                    });
            }
        }
        
        public ICommand ManageClientsOrProjectCommand
        { 
            get { return new RelayCommand(() => new ManageClientsOrProjectsWindow().Show()); }
        }
        
        public ICommand ExportToFileCommand
        {
            get
            {
                return new RelayCommand(() =>
                    {
                                                
                    });
            }
        }
        
        public ICommand WorkItemsListCommand
        {
            get
            {
                return new RelayCommand(() => new WorkItemsListWindow().Show());
            }
        }

        private void Reset()
        {
            WorkItem = new WorkItem();
        }

    }
}