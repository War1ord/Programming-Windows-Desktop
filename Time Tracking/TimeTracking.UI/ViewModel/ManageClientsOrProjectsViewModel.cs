using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using TimeTracking.Models;
using System.Windows;

namespace TimeTracking.UI.ViewModel
{
    public class ManageClientsOrProjectsViewModel : ViewModelBase
    {
		#region private fields
		private string _statusMessage;
		private ObservableCollection<ClientOrProject> _list;
		private ClientOrProject _selected; 
		#endregion

        public ManageClientsOrProjectsViewModel()
        {
            List = new ObservableCollection<ClientOrProject>(new Business.WorkItems().GetClientsOrProjectsList());
        }

        public ObservableCollection<ClientOrProject> List
        {
            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged(() => List);
            }
        }
        public ClientOrProject Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                RaisePropertyChanged(() => StatusMessage);
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (List == null) List = new ObservableCollection<ClientOrProject>();

                    List.Add(new ClientOrProject
                    {
                        Name = "New Item " + (List.Count + 1),
                    });
                });
            }
        }

	    public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (Selected != null)
                    {
                        var result = new Business.ClientsOrProjects().Update(Selected);
                        StatusMessage = result.Message;
                    }
                    else
                    {
                        StatusMessage = "Nothing is selected to update!";
                    }
                });
            }
        }

	    public ICommand DeleteCommand
	    {
		    get
		    {
			    return new RelayCommand(() =>
			    {
				    if (List != null && List.Any())
				    {
					    if (Selected != null)
					    {
						    var result = MessageBox.Show("Are you sure you want to delete this Client or Project?",
							    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
						    if (result == MessageBoxResult.Yes)
						    {
							    var deletion = new Business.ClientsOrProjects().Delete(Selected);
							    StatusMessage = deletion.Message;
							    if (deletion.IsSuccessful)
							    {
								    List.Remove(Selected);
								    Selected = null;
							    }
						    }
					    }
					    else
					    {
						    StatusMessage = "Nothing is selected";
					    }
				    }
				    else
				    {
					    StatusMessage = "Invalid operation.";
				    }
			    });
		    }
	    }

    }
}