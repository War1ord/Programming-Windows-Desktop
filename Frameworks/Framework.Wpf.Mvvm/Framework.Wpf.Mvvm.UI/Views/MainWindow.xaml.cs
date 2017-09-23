using System.Windows;
using Framework.Wpf.Mvvm.UI.ViewModels;

namespace Framework.Wpf.Mvvm.UI.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			DataContext = new MainViewModel();
			InitializeComponent();
		}
	}
}
