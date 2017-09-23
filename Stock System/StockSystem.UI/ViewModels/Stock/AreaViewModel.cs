using Framework.Wpf.Mvvm.Core.Commands;
using Framework.Wpf.Mvvm.UI.Helpers.Extentions;
using Framework.Wpf.Mvvm.UI.ViewModels.Base;
using StockSystem.Business;
using StockSystem.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Stock
{
	public class AreaViewModel : ViewModelBase
	{
		private AreaType _selectedType;
		private Area _area;

		public AreaViewModel()
		{
			Initialize();
		}
		public AreaViewModel(Window window, Window parentWindow) : base(window, parentWindow)
		{
			Initialize();
		}
		public AreaViewModel(Area area, Window window, Window parentWindow) : base(window, parentWindow)
		{
			Initialize(area);
		}
		private void Initialize(Area area = null)
		{
			Area = area ?? new Area();
			AreaTypes = DataManagement<AreaType>.GetList();
		}

		public Area Area
		{
			get { return _area; }
			set 
			{
				_area = value;
				if (value != null && value.Type != null)
				{
					SelectedType = value.Type;
				}
			}
		}

		public List<AreaType> AreaTypes { get; set; }

		public AreaType SelectedType
		{
			get { return _selectedType; }
			set
			{
				_selectedType = value;
				Area.Type = value;
				OnPropertyChanged(() => SelectedType);
			}
		}
		public ICommand SaveCommand { get { return new Command(Save); } }
		private void Save()
		{
			var validationResult = DataManagement<Area>.GetResult(Area);
			if (validationResult.IsValid)
			{
				var result = DataManagement<Area>.AddItem(Area);
				if (result.Type == ResultType.Success)
				{
					MessageBox.Show("Saved Area");
					CloseAndRefresh();
				}
				else
				{
					MessageBox.Show("Failed to save Area");
				}
			}
			else
			{
				MessageBox.Show(string.Format("Failed to save Area. \r\nThere was some validation errors. \r\n{0}", validationResult.ValidationErrors.ToErrorString()));
			}
		}
	}
}