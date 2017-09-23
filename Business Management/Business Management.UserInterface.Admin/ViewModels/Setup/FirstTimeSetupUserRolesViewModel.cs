using Business_Management.Business.Extentions;
using Business_Management.Models.User;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Business_Management.UserInterface.Admin.ViewModels.Setup
{
	public class FirstTimeSetupUserRolesViewModel : ViewModelBase
	{
		private UserRole selectedItem;
		private ObservableCollection<UserRole> list;

		public ObservableCollection<UserRole> List
		{
			get
			{
				return list ?? (list = new ObservableCollection<UserRole>());
			}
			set
			{
				list = value;
				RaisePropertyChanged(() => List);
			}
		}
		public UserRole SelectedItem
		{
			get
			{
				return selectedItem;
			}
			set
			{
				selectedItem = value;
				RaisePropertyChanged(() => SelectedItem);
				RaisePropertyChanged(() => IsItemValid);
			}
		}

		public bool IsItemValid
		{
			get
			{
				return SelectedItem.IsSet();
			}
		}

		public ICommand AddCommand { get { return new RelayCommand(Add, () => true); } }
		public ICommand RemoveCommand { get { return new RelayCommand(Remove, () => true); } }

		private void Add()
		{
			list.Add(new UserRole { Name = "new value " + (list.Count + 1) });
		}
		private void Remove()
		{
			list.Remove(SelectedItem);
		}

	}
}