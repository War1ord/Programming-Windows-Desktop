using Business_Management.Business.Extentions;
using Business_Management.Models.Product;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Business_Management.UserInterface.Admin.ViewModels.Setup
{
	public class FirstTimeSetupProductTypesViewModel : ViewModelBase
	{
		private ProductType selectedItem;
		private ObservableCollection<ProductType> list;

		public ObservableCollection<ProductType> List
		{
			get
			{
				return list ?? (list = new ObservableCollection<ProductType>());
			}
			set
			{
				list = value;
				RaisePropertyChanged(() => List);
			}
		}
		public ProductType SelectedItem
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
			list.Add(new ProductType { Name = "new value " + (list.Count + 1) });
		}
		private void Remove()
		{
			list.Remove(SelectedItem);
		}

	}
}