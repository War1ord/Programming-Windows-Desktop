using System.Windows;
using System.Windows.Controls;
using TimeTracking.UI.Mvvm;

namespace TimeTracking.UI.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Closing += (sender, args) => ViewModelLocator.Cleanup();
		}

	    private void ToggleButton_Click(object sender, RoutedEventArgs e)
	    {
            var toggleButtonTopMost = (CheckBox)sender;
            Topmost = toggleButtonTopMost.IsChecked.HasValue && toggleButtonTopMost.IsChecked.Value;
	    }
	}
}