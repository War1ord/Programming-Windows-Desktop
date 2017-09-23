using System.Windows;
using System.Windows.Input;

namespace UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			//if (e.Key == Key.S && (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt))
			//{
			//    var mainViewModel = DataContext as ViewModel.MainViewModel;
			//    if (mainViewModel != null)
			//    {
			//        mainViewModel.SendEmail.Execute(null);
			//    }
			//}
		}

		private void Password_PasswordChanged(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as ViewModel.MainViewModel;
			if (vm != null)
			{
				vm.Password = Password.Password;
			}
		}

		private void SmtpPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as ViewModel.MainViewModel;
			if (vm != null)
			{
				vm.SmtpPassword = SmtpPassword.Password;
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as ViewModel.MainViewModel;
			if (vm != null)
			{
				Password.Password = vm.Password;
				SmtpPassword.Password = vm.SmtpPassword;
			}
		}
	}
}
