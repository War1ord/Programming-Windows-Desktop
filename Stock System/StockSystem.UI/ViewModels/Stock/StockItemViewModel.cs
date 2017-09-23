using System.Windows;
using System.Windows.Input;
using Framework.Wpf.Mvvm.Core.Commands;
using Framework.Wpf.Mvvm.UI.Helpers;
using Framework.Wpf.Mvvm.UI.Helpers.Extentions;
using Framework.Wpf.Mvvm.UI.ViewModels.Base;
using StockSystem.Business;
using StockSystem.Models;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Stock
{
	public class StockItemViewModel : ViewModelBase
	{
		private StockTypeGroup _selectedStockTypeGroup;
		private AreaType _selectedAreaType;
		private StockType _selectedStockType;
		private Area _selectedArea;
		private StockItem _stockItem;

		public StockItemViewModel()
		{
			Initialize();
		}
		public StockItemViewModel(Window window, Window parentWindow) : base(window, parentWindow)
		{
			Initialize();
		}

		public StockItemViewModel(StockItem stockItem, Window window, Window parentWindow) : base(window, parentWindow)
		{
			Initialize(stockItem);
		}

		private void Initialize(StockItem stockItem = null)
		{
			using (var manager = new StockItemsManager())
			{
				AreaTypes = new CustomObservableCollection<AreaType>(manager.GetAreaTypesIncludingAreas());
				StockTypeGroups = new CustomObservableCollection<StockTypeGroup>(manager.GetStockTypeGroupsIncludingStockTypes());
				Areas = new CustomObservableCollection<Area>();
				StockTypes = new CustomObservableCollection<StockType>();
				StockItem = stockItem ?? new StockItem();
			}
		}

		public StockItem StockItem
		{
			get { return _stockItem; }
			set
			{
				_stockItem = value;
				if (value != null)
				{
					if (value.Area != null)
					{
						if (value.Area.Type != null)
						{
							SelectedAreaType = value.Area.Type;
						}
						SelectedArea = value.Area;
					}
					if (value.Type != null)
					{
						if (value.Type.Group != null)
						{
							SelectedStockTypeGroup = value.Type.Group;
						}
						SelectedStockType = value.Type;
					}
				}
				OnPropertyChanged(() => StockItem);
			}
		}

		public CustomObservableCollection<StockType> StockTypes { get; set; }
		public CustomObservableCollection<StockTypeGroup> StockTypeGroups { get; set; }
		public CustomObservableCollection<Area> Areas { get; set; }
		public CustomObservableCollection<AreaType> AreaTypes { get; set; }

		public StockTypeGroup SelectedStockTypeGroup
		{
			get { return _selectedStockTypeGroup; }
			set
			{
				_selectedStockTypeGroup = value;
				StockTypes.Repopulate(_selectedStockTypeGroup.StockTypes);
				OnPropertyChanged(() => SelectedStockTypeGroup);
			}
		}
		public StockType SelectedStockType
		{
			get { return _selectedStockType; }
			set
			{
				_selectedStockType = value;
				StockItem.Type = value;
				OnPropertyChanged(() => SelectedStockType);
			}
		}
		public AreaType SelectedAreaType
		{
			get { return _selectedAreaType; }
			set
			{
				_selectedAreaType = value;
				Areas.Repopulate(_selectedAreaType.Areas);
				OnPropertyChanged(() => SelectedAreaType);
			}
		}
		public Area SelectedArea
		{
			get { return _selectedArea; }
			set
			{
				_selectedArea = value;
				StockItem.Area = value;
				OnPropertyChanged(() => SelectedArea);
			}
		}
		public ICommand SaveCommand { get { return new Command(Save); } }
		private void Save()
		{
			var validationResult = DataManagement<StockItem>.GetResult(StockItem);
			if (validationResult.IsValid)
			{
				var result = DataManagement<StockItem>.AddItem(StockItem);
				if (result.Type == ResultType.Success)
				{
					MessageBox.Show("Saved Stock Item");
					CloseAndRefresh();
				}
				else
				{
					MessageBox.Show("Failed to save Stock item");
				}
			}
			else
			{
				MessageBox.Show(string.Format("Failed to save Stock item. \r\nThere was some validation errors. \r\n{0}", validationResult.ValidationErrors.ToErrorString()));
			}
		}
	}
}