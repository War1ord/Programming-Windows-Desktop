using System.Windows;
using System.Windows.Input;

namespace Recipes
{
	/// <summary>
	/// Interaction logic for MainViewRecipes.xaml
	/// </summary>
	public partial class MainViewRecipes : Window
	{
		public MainViewRecipes()
		{
			InitializeComponent();
		}

		private void OnDragMoveWindow(object sender, MouseButtonEventArgs e) { DragMove(); }
		private void OnMinimizeWindow(object sender, MouseButtonEventArgs e) { WindowState = WindowState.Minimized; }
		private void OnMaximizeWindow(object sender, MouseButtonEventArgs e)
		{
			switch (WindowState)
			{
				case WindowState.Maximized:
					WindowState = WindowState.Normal;
					break;
				case WindowState.Normal:
					WindowState = WindowState.Maximized;
					break;
			}
		}
		private void OnCloseWindow(object sender, MouseButtonEventArgs e) { Close(); }
	}
}
