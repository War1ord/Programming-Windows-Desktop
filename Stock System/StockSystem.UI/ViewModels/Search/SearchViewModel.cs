using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Framework.Wpf.Mvvm.Core.Commands;
using Framework.Wpf.Mvvm.UI.Helpers;
using Framework.Wpf.Mvvm.UI.ViewModels.Base;
using StockSystem.Business;
using StockSystem.Models;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Search
{
	public class SearchViewModel : ContentViewModelBase
	{
		#region Fields
		private string _windowTitle;
		private StockItem _selectedStockItem;
		private StockType _selectedStockType;
		private StockTypeGroup _selectedStockTypeGroup;
		private Area _selectedArea;
		private AreaType _selectedAreaType;
		private string _nameFilter;
		private CustomObservableCollection<StockItem> _list;
		private CustomObservableCollection<Area> _areas;
		private CustomObservableCollection<AreaType> _areaTypes;
		private CustomObservableCollection<StockType> _stockTypes;
		private CustomObservableCollection<StockTypeGroup> _stockTypeGroups;
		#endregion

		public SearchViewModel(Window window, Window parentWindow) : base(window, parentWindow)
		{
			using (var manager = new StockItemsManager())
			{
				StockTypeGroups = GetStockTypeGroups(manager);
				AreaTypes = GetAreaTypes(manager);
				List = GetStockItemsList(manager);
			}
			Areas = new CustomObservableCollection<Area>();
			StockTypes = new CustomObservableCollection<StockType>();
		}

		public string WindowTitle
		{
			get { return _windowTitle; }
			set
			{
				_windowTitle = value;
				OnPropertyChanged(() => WindowTitle);
			}
		}
		public string NameFilter
		{
			get { return _nameFilter; }
			set
			{
				_nameFilter = value; 
				RefreshList();
				OnPropertyChanged(() => NameFilter);
			}
		}

		public int StockItemsCount
		{
			get { return List.Count; }
		}

		#region Collection Properties
		public CustomObservableCollection<StockItem> List
		{
			get { return _list; }
			set 
			{ 
				_list = value; 
				OnPropertyChanged(() => List);
				OnPropertyChanged(() => StockItemsCount);
			}
		}
		public CustomObservableCollection<Area> Areas
		{
			get { return _areas; }
			set { _areas = value; OnPropertyChanged(() => Areas);}
		}
		public CustomObservableCollection<AreaType> AreaTypes
		{
			get { return _areaTypes; }
			set { _areaTypes = value; OnPropertyChanged(() => AreaTypes);}
		}
		public CustomObservableCollection<StockType> StockTypes
		{
			get { return _stockTypes; }
			set { _stockTypes = value; OnPropertyChanged(() => StockTypes);}
		}
		public CustomObservableCollection<StockTypeGroup> StockTypeGroups
		{
			get { return _stockTypeGroups; }
			set { _stockTypeGroups = value; OnPropertyChanged(() => StockTypeGroups);}
		}
		#endregion

		#region Selected Properties
		public StockItem SelectedStockItem
		{
			get { return _selectedStockItem; }
			set
			{
				_selectedStockItem = value;
				OnPropertyChanged(() => SelectedStockItem);
			}
		}
		public AreaType SelectedAreaType
		{
			get { return _selectedAreaType; }
			set
			{
				_selectedAreaType = value;
				Areas.Repopulate(_selectedAreaType != null 
					? _selectedAreaType.Areas : new List<Area>());
				RefreshList();
				OnPropertyChanged(() => SelectedAreaType);
			}
		}
		public Area SelectedArea
		{
			get { return _selectedArea; }
			set
			{
				_selectedArea = value;
				RefreshList();
				OnPropertyChanged(() => SelectedArea);
			}
		}
		public StockTypeGroup SelectedStockTypeGroup
		{
			get { return _selectedStockTypeGroup; }
			set
			{
				_selectedStockTypeGroup = value;
				StockTypes.Repopulate(_selectedStockTypeGroup != null 
					? _selectedStockTypeGroup.StockTypes : new List<StockType>());
				RefreshList();
				OnPropertyChanged(() => SelectedStockTypeGroup);
				OnPropertyChanged(() => StockTypes);
			}
		}
		public StockType SelectedStockType
		{
			get { return _selectedStockType; }
			set
			{
				_selectedStockType = value;
				RefreshList();
				OnPropertyChanged(() => SelectedStockType);
			}
		}
		#endregion

		#region Commands
		public ICommand ClearSelectedAreaTypeCommand
		{
			get { return new Command(() => SelectedAreaType = null); }
		}
		public ICommand ClearSelectedAreaCommand
		{
			get { return new Command(() => SelectedArea = null); }
		}
		public ICommand ClearSelectedStockTypeGroupCommand
		{
			get { return new Command(() => SelectedStockTypeGroup = null); }
		}
		public ICommand ClearSelectedStockTypeCommand
		{
			get { return new Command(() => SelectedStockType = null); }
		}

		#endregion

		#region Helpers
		private static CustomObservableCollection<StockTypeGroup> GetStockTypeGroups(StockItemsManager manager)
		{
			return new CustomObservableCollection<StockTypeGroup>(manager.GetStockTypeGroupsIncludingStockTypes());
		}
		private CustomObservableCollection<StockItem> GetStockItemsList(StockItemsManager manager)
		{
			return new CustomObservableCollection<StockItem>(manager.GetListByFilters(SelectedArea, SelectedAreaType, SelectedStockTypeGroup, SelectedStockType, NameFilter));
		}
		private static CustomObservableCollection<AreaType> GetAreaTypes(StockItemsManager manager)
		{
			return new CustomObservableCollection<AreaType>(manager.GetAreaTypesIncludingAreas());
		}

		public void RefreshList()
		{
			using (var manager = new StockItemsManager())
			{
				List = GetStockItemsList(manager);
			}
		}
		public void RefreshAreaTypes()
		{
			using (var manager = new StockItemsManager())
			{
				AreaTypes = GetAreaTypes(manager);
			}
		}
		public void RefreshStockTypeGroups()
		{
			using (var manager = new StockItemsManager())
			{
				StockTypeGroups = GetStockTypeGroups(manager);
			}
		}
		#endregion

	}
}