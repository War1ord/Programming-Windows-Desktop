using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace Business_Management.UserInterface.Admin
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : System.Windows.Application
	{
		private Container components;
		private NotifyIcon notifyIcon;
		private ToolStripItem[] defaultMenuItems = new ToolStripItem[]
		{
			new ToolStripMenuItem("Open Main Window", null, delegate { }),
			new ToolStripSeparator(),
			new ToolStripMenuItem("E&xit", null, delegate { Environment.Exit(0); }),
		};

		public App()
		{
			//DataContext.InitializeDatabase(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

			components = new Container();
			var contextMenuStrip = new ContextMenuStrip();
			contextMenuStrip.Items.AddRange(defaultMenuItems);
			notifyIcon = new NotifyIcon(components)
			{
				ContextMenuStrip = contextMenuStrip,
				Icon = Admin.Properties.Resources.ApplicationIcon,
				Text = Admin.Properties.Settings.Default.ApplicationIconToolTip,
				Visible = true
			};
			notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
			notifyIcon.DoubleClick += notifyIcon_DoubleClick;
		}

		private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			var menu = sender as ContextMenuStrip;
			if (menu != null)
			{
				menu.Items.Clear();
				// Re-Build Menu, NOTE: items are added to the bottom of the menu list. So Exit must be added last.
				menu.Items.Add(new ToolStripMenuItem("test 123")
				{
					DropDownItems =
					{
						new ToolStripMenuItem("test1"),
						new ToolStripMenuItem("test2") { Enabled=false },
						new ToolStripMenuItem("test3") { Enabled=false },
					}
				});
				menu.Items.Add("Change database connection");
				menu.Items.Add("Run First time setup");
				// add the default menu items to the bottom. 
				menu.Items.AddRange(defaultMenuItems);
			}
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			// maybe minimize all and show what is minimized, or ....
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			if (notifyIcon != null) notifyIcon.Dispose();
			notifyIcon = null;
			if (components != null) components.Dispose();
			components = null;
		}

	}
}
