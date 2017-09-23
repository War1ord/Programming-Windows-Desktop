using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using Work_Tracker.Business;
using Work_Tracker.Business.Models;
using Work_Tracker.Windows;

namespace Work_Tracker.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand _file_new_command;
        public RelayCommand _file_open_command;
        public RelayCommand _file_exit_command;
        public RelayCommand _workitem_add_command;
        public RelayCommand _workitem_remove_command;
        public RelayCommand _defaultcheck_add_command;
        public RelayCommand _defaultcheck_remove_command;
        public RelayCommand _step_add_command;
        public RelayCommand _step_remove_command;
        public RelayCommand _check_add_command;
        public RelayCommand _check_remove_remove;
        public ObservableCollection<WorkItem> _items;
        public WorkItem _selected_item;
        public Check _selected_default_check;
        public Step _selected_step;
        public Check _selected_check;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Items = new ObservableCollection<WorkItem>();
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            Load();
        }

        private void Load()
        {
            //TODO: add code for loading data
        }

        private Result<string> GetDescription(string title, string message)
        {
            return GetDescription(title, message, null);
        }
        private Result<string> GetDescription(string title, string message, string current_description)
        {
            //TODO: add code to create a new window that requests you to enter a description. 
            var dialog = new EnterDescription(new EnterDescriptionViewModel
            {
                Title = title,
                Message = message,
                Description = current_description,
            });
            var result = dialog.ShowDialog();
            var description = dialog.Data.Description;
            var resultType = result.HasValue && result.Value ? ResultType.Success : ResultType.Error;
            return new Result<string>(resultType, resultType.ToString(), description);
        }

        public RelayCommand File_New_Command
        {
            get
            {
                return _file_new_command ?? (_file_new_command = new RelayCommand(() =>
                {
                    Items = new ObservableCollection<WorkItem>();
                    RaisePropertyChanged(() => Items);
                }));
            }
        }
        public RelayCommand File_Open_Command
        {
            get
            {
                return _file_open_command ?? (_file_open_command = new RelayCommand(() =>
                {

                }));
            }
        }
        public RelayCommand File_Exit_Command
        {
            get
            {
                return _file_exit_command = (_file_exit_command = new RelayCommand(() =>
                {
                    Application.Current.Shutdown();
                }));
            }
        }
        public RelayCommand WorkItem_Add_Command
        {
            get
            {
                return _workitem_add_command = (_workitem_add_command = new RelayCommand(() =>
                {
                    var result = GetDescription("Enter a description", "Please enter the work item description?");
                    if (result.Type == ResultType.Success)
                    {
                        Items.Add(new WorkItem
                        {
                            Description = result.Return,
                        });
                    }
                }
                , () => Items != null));
            }
        }
        public RelayCommand WorkItem_Remove_Command
        {
            get
            {
                return _workitem_remove_command ?? (_workitem_remove_command = new RelayCommand(() =>
                {
                    Items.Remove(SelectedItem);
                }
                , () => Items != null));
            }
        }
        public RelayCommand DefaultCheck_Add_Command
        {
            get
            {
                return _defaultcheck_add_command ?? (_defaultcheck_add_command = new RelayCommand(() =>
                {
                    var result = GetDescription("Enter a description", "Please enter the default check's description?");
                    if (result.Type == ResultType.Success)
                    {
                        SelectedItem.DefaultChecks.Add(new Check
                        {
                            Description = result.Return,
                            IsDefault = true,
                        });
                        RaisePropertyChanged(() => SelectedItem);
                    }
                }
                , () => SelectedItem != null));
            }
        }
        public RelayCommand EditDefaultCheck_Remove_Command
        {
            get
            {
                return _defaultcheck_remove_command ?? (_defaultcheck_remove_command = new RelayCommand(() =>
                {
                    SelectedItem.DefaultChecks.Remove(SelectedDefaultCheck);
                }
                , () => SelectedItem != null
                    && SelectedItem.DefaultChecks != null
                    && SelectedDefaultCheck != null));
            }
        }
        public RelayCommand Step_Add_Command
        {
            get
            {
                return _step_add_command ?? (_step_add_command = new RelayCommand(() =>
                {
                    var result = GetDescription("Enter a description", "Please enter the description of the step?");
                    if (result.Type == ResultType.Success)
                    {
                        SelectedItem.Steps.Add(new Step
                        {
                            Checks = SelectedItem.DefaultChecks,
                            Description = result.Return,
                        });
                    }
                }));
            }
        }
        public RelayCommand Step_Remove_Command
        {
            get
            {
                return _step_remove_command ?? (_step_remove_command = new RelayCommand(() =>
                {
                    SelectedItem.Steps.Remove(SelectedStep);
                }));
            }
        }
        public RelayCommand Check_Add_Command
        {
            get
            {
                return _check_add_command ?? (_check_add_command = new RelayCommand(() =>
                {
                    var result = GetDescription("Enter a description", "Please enter the check's description?");
                    if (result.Type == ResultType.Success)
                    {
                        SelectedStep.Checks.Add(new Check
                        {
                            Description = result.Return,
                        });
                    }
                }));
            }
        }
        public RelayCommand Check_Remove_Remove
        {
            get
            {
                return _check_remove_remove ?? (_check_remove_remove = new RelayCommand(() =>
                {
                    SelectedStep.Checks.Remove(SelectedCheck);
                }));
            }
        }

        public ObservableCollection<WorkItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        public WorkItem SelectedItem
        {
            get { return _selected_item; }
            set
            {
                _selected_item = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }
        public Check SelectedDefaultCheck
        {
            get { return _selected_default_check; }
            set
            {
                _selected_default_check = value;
                RaisePropertyChanged(() => SelectedDefaultCheck);
            }
        }
        public Step SelectedStep
        {
            get { return _selected_step; }
            set
            {
                _selected_step = value;
                RaisePropertyChanged(() => SelectedStep);
            }
        }
        public Check SelectedCheck
        {
            get { return _selected_check; }
            set
            {
                _selected_check = value;
                RaisePropertyChanged(() => SelectedCheck);
            }
        }

        public ObservableCollection<Check> SelectedItem_DefaultChecks
        {
            get { return new ObservableCollection<Check>(SelectedItem.DefaultChecks); }
        }
        public ObservableCollection<Step> SelectedItem_Steps
        {
            get { return new ObservableCollection<Step>(SelectedItem.Steps); }
        }
        public ObservableCollection<Check> SelectedStep_Checks
        {
            get { return new ObservableCollection<Check>(SelectedStep.Checks); }
        }

    }
}