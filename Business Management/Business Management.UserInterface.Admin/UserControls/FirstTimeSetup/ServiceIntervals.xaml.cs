using Business_Management.Business.Extentions;
using Business_Management.UserInterface.Admin.ViewModels.Setup;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Business_Management.UserInterface.Admin.UserControls.FirstTimeSetup
{
	/// <summary>
	/// Interaction logic for ServiceIntervals.xaml
	/// </summary>
	public partial class ServiceIntervals : UserControl
	{
		public ServiceIntervals()
		{
			InitializeComponent();
		}

		private void TimePicker_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
		{
			var timePicker = sender as TimePicker;
			var model = DataContext as FirstTimeSetupServiceIntervalsViewModel;
			if (timePicker.IsSet() && timePicker.Value.HasValue && model.IsSet())
			{
				model.SelectedItem.Interval = timePicker.Value.Value.TimeOfDay;
				model.OnSelectedItemPropertiesChanged();
			}
		}
	}
}
