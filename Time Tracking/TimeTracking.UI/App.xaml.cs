using GalaSoft.MvvmLight.Threading;
using System;
using System.Windows;
using System.Windows.Forms;
using TimeTracking.Business.Data;
using TimeTracking.UI.Views;

namespace TimeTracking.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		static App()
		{
			DispatcherHelper.Initialize();
		}

		/// <summary>
		/// Initializes the notify icon.
		/// </summary>
		private void InitializeNotifyIcon()
		{
            DataContext.Initialize();
		    AppController.Instance.Notification = new NotifyIcon
		    {
		        Text = "Time Tracking",
		        Icon = IconResource.TimeTrackingIcon,
		        Visible = true,
		    };
			AppController.Instance.Notification.Click += Notification_Click;
			AppController.Instance.Notification.ContextMenu = new ContextMenu(new[] 
            {
                new MenuItem("&Show application", (sender, args) => MainWindow.Show()),
                new MenuItem("&Hide application", (sender, args) => MainWindow.Hide()),
                new MenuItem("-"),
                new MenuItem("&Manage Clients or Projects", (sender, args) => new ManageClientsOrProjectsWindow().Show()),
                new MenuItem("-"),
                new MenuItem("View &All (with filters)", (sender, args) => new WorkItemsListWindow().Show()),
                new MenuItem("-"),
                new MenuItem("E&xit", (sender, args) => Shutdown())
            });
		}

		void Notification_Click(object sender, EventArgs e)
		{
		    MainWindow.Show();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			InitializeNotifyIcon();
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			if (AppController.Instance.Notification != null)
			{
				AppController.Instance.Notification.Dispose();
			}
		}

	}
}
