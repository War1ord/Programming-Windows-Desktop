using System.Windows;
using System.Windows.Input;
using Framework.Wpf.Mvvm.Core.Commands;
using Framework.Wpf.Mvvm.UI.Helpers.Extentions;
using Framework.Wpf.Mvvm.UI.ViewModels.Base;
using StockSystem.Business;
using StockSystem.Models;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Stock
{
	public class AreaTypeViewModel : ViewModelBase
	{
		public AreaTypeViewModel(Window window, Window parentWindow) : base(window, parentWindow)
		{
			Window = window;
			AreaType = new AreaType();
		}

		public AreaTypeViewModel(AreaType areaType, Window window, Window parentWindow)
			: base(window, parentWindow)
		{
			Window = window;
			AreaType = areaType ?? new AreaType();
		}

		public AreaTypeViewModel()
		{
			AreaType = new AreaType();
		}

		public AreaType AreaType { get; set; }

		public ICommand SaveCommand { get { return new Command(Save); } }
		private void Save()
		{
			var validationResult = DataManagement<AreaType>.GetResult(AreaType);
			if (validationResult.IsValid)
			{
				var result = DataManagement<AreaType>.AddItem(AreaType);
				if (result.Type == ResultType.Success)
				{
					MessageBox.Show("Saved Area Type");
					CloseAndRefresh();
				}
				else
				{
					MessageBox.Show("Failed to save Area Type");
				}
			}
			else
			{
				MessageBox.Show(string.Format("Failed to save Area Type. \r\nThere was some validation errors.\r\n{0}", validationResult.ValidationErrors.ToErrorString()));
			}
		}
	}
}