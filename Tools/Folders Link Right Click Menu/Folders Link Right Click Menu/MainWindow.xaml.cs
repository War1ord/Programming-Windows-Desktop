using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace Folders_Link.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		    DataContext = new ViewModels.MainWindowViewModel();
		}
	}
}
