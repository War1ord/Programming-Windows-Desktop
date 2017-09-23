using System.Windows;
using Framework.Wpf.Mvvm.UI.ViewModels;

namespace Framework.Wpf.Mvvm.UI.Views
{
	/// <summary>
	/// Interaction logic for StyleTestWindow.xaml
	/// </summary>
	public partial class StyleTestWindow : Window
	{
		public StyleTestWindow()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}
	}
}
