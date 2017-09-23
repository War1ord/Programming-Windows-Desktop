using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Framework.Wpf.Mvvm.Core.Commands;
using Framework.Wpf.Mvvm.UI.Helpers.Extentions;
using Framework.Wpf.Mvvm.UI.ViewModels.Base;
using StockSystem.Business;
using StockSystem.Models;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Stock
{
	public class StockTypeViewModel : ViewModelBase
	{
		private StockTypeGroup _selectedGroup;
		private StockType _stockType;

		public StockTypeViewModel()
		{
			Initialize();
		}
		public StockTypeViewModel(Window window, Window parentWindow) : base(window, parentWindow)
		{
			Initialize();
		}
		public StockTypeViewModel(StockType stockType, Window window, Window parentWindow) : base(window, parentWindow)
		{
			Initialize(stockType);
		}
		private void Initialize(StockType stockType = null)
		{
			StockType = stockType ?? new StockType();
			StockTypeGroups = DataManagement<StockTypeGroup>.GetList();
		}

		public StockType StockType
		{
			get { return _stockType; }
			set
			{
				_stockType = value;
				if (value != null && value.Group != null)
				{
					SelectedGroup = value.Group;
				}
				OnPropertyChanged(() => StockType);
			}
		}

		public List<StockTypeGroup> StockTypeGroups { get; set; }

		public StockTypeGroup SelectedGroup
		{
			get { return _selectedGroup; }
			set
			{
				_selectedGroup = value;
				StockType.Group = value;
				OnPropertyChanged(() => SelectedGroup);
			}
		}

		public ICommand SaveCommand { get { return new Command(Save); } }
		private void Save()
		{
			var validationResult = DataManagement<StockType>.GetResult(StockType);
			if (validationResult.IsValid)
			{
				var result = DataManagement<StockType>.AddItem(StockType);
				if (result.Type == ResultType.Success)
				{
					MessageBox.Show("Saved Stock Type");
					CloseAndRefresh();
				}
				else
				{
					MessageBox.Show("Failed to save Stock Type");
				}
			}
			else
			{
				MessageBox.Show(string.Format("Failed to save Stock Type. \r\nThere was some validation errors. \r\n{0}", validationResult.ValidationErrors.ToErrorString()));
			}
		}
	}
}