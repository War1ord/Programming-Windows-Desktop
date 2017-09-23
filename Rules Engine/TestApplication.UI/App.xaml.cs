using System.Windows;

namespace TestApplication.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			Business.DataContextHelper.Initialize();
			base.OnStartup(e);
		}
	}
}
