using Business_Management.Business.Extentions;
using Business_Management.Models.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Business_Management.UserInterface.Admin.ViewModels.Setup
{
	public class FirstTimeSetupServiceIntervalsViewModel : ViewModelBase
	{
		private ServiceInterval selectedItem;
		private ObservableCollection<ServiceInterval> list;

		public ObservableCollection<ServiceInterval> List
		{
			get
			{
				return list ?? (list = new ObservableCollection<ServiceInterval>());
			}
			set
			{
				list = value;
				RaisePropertyChanged(() => List);
			}
		}
		public ServiceInterval SelectedItem
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

		public void OnSelectedItemPropertiesChanged()
		{
			RaisePropertyChanged(() => SelectedItem);
			RaisePropertyChanged(() => SelectedItem.Name);
			RaisePropertyChanged(() => SelectedItem.Interval);
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
			list.Add(new ServiceInterval { Name = "new value " + (list.Count + 1) });
		}
		private void Remove()
		{
			list.Remove(SelectedItem);
		}
	}
}