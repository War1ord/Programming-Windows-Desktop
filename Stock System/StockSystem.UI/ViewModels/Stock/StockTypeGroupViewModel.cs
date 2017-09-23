using System.Windows;
using System.Windows.Input;
using Framework.Wpf.Mvvm.Core.Commands;
using Framework.Wpf.Mvvm.UI.Helpers.Extentions;
using Framework.Wpf.Mvvm.UI.ViewModels.Base;
using StockSystem.Business;
using StockSystem.Models;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Stock
{
	public class StockTypeGroupViewModel : ViewModelBase
	{
		public StockTypeGroupViewModel()
		{
			Initialize();
		}
		public StockTypeGroupViewModel(Window window, Window parentWindow) : base(window, parentWindow)
		{
			Initialize();
		}

		public StockTypeGroupViewModel(StockTypeGroup stockTypeGroup, Window window, Window parentWindow) : base(window, parentWindow)
		{
			Group = stockTypeGroup;
		}

		private void Initialize()
		{
			Group = new StockTypeGroup();
		}

		public StockTypeGroup Group { get; set; }

		public ICommand SaveCommand { get { return new Command(Save); } }
		private void Save()
		{
			var validationResult = DataManagement<StockTypeGroup>.GetResult(Group);
			if (validationResult.IsValid)
			{
				var result = DataManagement<StockTypeGroup>.AddItem(Group);
				if (result.Type == ResultType.Success)
				{
					MessageBox.Show("Saved Stock Type Group");
					CloseAndRefresh();
				}
				else
				{
					MessageBox.Show("Failed to save Stock Type Group.");
				}
			}
			else
			{
				MessageBox.Show(string.Format("Failed to save Stock Type Group. \r\nThere was some validation errors. \r\n{0}", validationResult.ValidationErrors.ToErrorString()));
			}
		}
	}
}