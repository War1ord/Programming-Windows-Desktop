using System.Windows;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Base
{
	public abstract class ContentViewModelBase : ViewModelBase
	{
		protected ContentViewModelBase(Window window, Window parentWindow) : base(window, parentWindow)
		{
		}
	}
}