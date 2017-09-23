using Framework.Wpf.Mvvm.Core.ViewModels;
using Framework.Wpf.Mvvm.Core.ViewModels.Base;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Content
{
	public class HeaderViewModel : ContentBase
	{
		public HeaderViewModel()
		{
			WindowTitle = "Wpf Mvvm Framework";
		}

		public string WindowTitle { get; set; }
	}
}