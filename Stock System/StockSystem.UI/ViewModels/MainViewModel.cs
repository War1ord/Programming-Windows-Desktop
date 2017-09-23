using Framework.Wpf.Mvvm.Core.Commands;
using Framework.Wpf.Mvvm.UI.UserControls.Stock;
using Framework.Wpf.Mvvm.UI.ViewModels.Base;
using Framework.Wpf.Mvvm.UI.ViewModels.Search;
using Framework.Wpf.Mvvm.UI.ViewModels.Stock;
using System;
using System.Windows;
using System.Windows.Input;

namespace Framework.Wpf.Mvvm.UI.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private string _footerDescription;
		private const string AppTitle = "Stock System";

		public MainViewModel(Window window, Window parentWindow)
			: base(window, parentWindow)
		{
			FooterDescription = string.Format("{0} {1}", AppTitle, DateTime.Now.ToString("f"));
			InitializeViews();
		}

		private void InitializeViews()
		{
			SearchViewModel = new SearchViewModel(Window, null)
			{
				WindowTitle = AppTitle,
			};
		}

		public SearchViewModel SearchViewModel { get; set; }

		public string FooterDescription
		{
			get { return _footerDescription; }
			set { _footerDescription = value; OnPropertyChanged(() => FooterDescription); }
		}

		public ICommand AddAreaTypeCommand { get { return new Command(AddAreaType); } }
		public ICommand AddAreaCommand { get { return new Command(AddArea); } }
		public ICommand AddStockTypeGroupCommand { get { return new Command(AddStockTypeGroup); } }
		public ICommand AddStockTypeCommand { get { return new Command(AddStockType); } }
		public ICommand AddStockItemCommand { get { return new Command(AddStockItem); } }

		private void AddAreaType()
		{
			NewWindow<AreaTypeUserControl, AreaTypeViewModel>(Window, "Add an Area Type").ShowDialog();
		}
		private void AddArea()
		{
			NewWindow<AreaUserControl, AreaViewModel>(Window, "Add an Area").ShowDialog();
		}
		private void AddStockTypeGroup()
		{
			NewWindow<StockTypeGroupUserControl, StockTypeGroupViewModel>(Window, "Add a Stock Type Group").ShowDialog();
		}
		private void AddStockType()
		{
			NewWindow<StockTypeUserControl, StockTypeViewModel>(Window, "Add a Stock Type").ShowDialog();
		}
		private void AddStockItem()
		{
			NewWindow<StockItemUserControl, StockItemViewModel>(Window, "Add a Stock Item").ShowDialog();
		}

		public ICommand EditAreaTypeCommand { get { return new Command(EditAreaType); ; } }
		public ICommand EditAreaCommand { get { return new Command(EditArea); ; } }
		public ICommand EditStockTypeGroupCommand { get { return new Command(EditStockTypeGroup); ; } }
		public ICommand EditStockTypeCommand { get { return new Command(EditStockType); ; } }
		public ICommand EditStockItemCommand { get { return new Command(EditStockItem); ; } }

		private void EditAreaType()
		{
			NewWindow<AreaTypeUserControl, AreaTypeViewModel>(Window, "Add an Area Type", new AreaTypeViewModel(SearchViewModel.SelectedAreaType, Window, ParentWindow)).ShowDialog();
		}
		private void EditArea()
		{
			NewWindow<AreaUserControl, AreaViewModel>(Window, "Add an Area", new AreaViewModel(SearchViewModel.SelectedArea, Window, ParentWindow)).ShowDialog();
		}
		private void EditStockTypeGroup()
		{
			NewWindow<StockTypeGroupUserControl, StockTypeGroupViewModel>(Window, "Add a Stock Type Group", new StockTypeGroupViewModel(SearchViewModel.SelectedStockTypeGroup, Window, ParentWindow)).ShowDialog();
		}
		private void EditStockType()
		{
			NewWindow<StockTypeUserControl, StockTypeViewModel>(Window, "Add a Stock Type", new StockTypeViewModel(SearchViewModel.SelectedStockType, Window, ParentWindow)).ShowDialog();
		}
		private void EditStockItem()
		{
			NewWindow<StockItemUserControl, StockItemViewModel>(Window, "Add a Stock Item", new StockItemViewModel(SearchViewModel.SelectedStockItem, Window, ParentWindow)).ShowDialog();
		}
	}
}