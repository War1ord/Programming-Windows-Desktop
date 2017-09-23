using System.Windows;
using System.Windows.Controls;

namespace Business_Management.UserInterface.Admin.Windows.Setup
{
	/// <summary>
	/// Interaction logic for ConnectionSetup.xaml
	/// </summary>
	public partial class ConnectionSetup : Window
	{
		public ConnectionSetup()
		{
			InitializeComponent();
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (DataContext != null)
			{
				((dynamic)DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
			}
		}
	}
}
