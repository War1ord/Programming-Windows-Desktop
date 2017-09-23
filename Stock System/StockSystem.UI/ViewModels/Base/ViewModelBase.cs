using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Framework.Wpf.Mvvm.Core.Commands;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Base
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		protected ViewModelBase()
		{
		}
		protected ViewModelBase(Window window, Window parentWindow)
		{
			Window = window;
			ParentWindow = parentWindow;
		}

		protected Window NewWindow<TUserControl, TViewModel>(Window parentWindow, string title)
			where TUserControl : UserControl, new()
			where TViewModel : ViewModelBase, new()
		{
			var window = new Window
			{
				Title = title,
				SizeToContent = SizeToContent.WidthAndHeight,
				ResizeMode = ResizeMode.NoResize,
				WindowStyle = WindowStyle.ToolWindow,
				WindowStartupLocation = WindowStartupLocation.CenterScreen,
			};
			var viewModel = new TViewModel
			{
				Window = window,
				ParentWindow = parentWindow,
			};
			var userControl = new TUserControl
			{
				DataContext = viewModel
			};
			window.Content = userControl;
			return window;
		}

		protected Window NewWindow<TUserControl, TViewModel>(Window parentWindow, string title, TViewModel viewModel)
			where TUserControl : UserControl, new()
			where TViewModel : ViewModelBase, new()
		{
			var window = new Window
			{
				Title = title,
				SizeToContent = SizeToContent.WidthAndHeight,
				ResizeMode = ResizeMode.NoResize,
				WindowStyle = WindowStyle.ToolWindow,
				WindowStartupLocation = WindowStartupLocation.CenterScreen,
			};
			viewModel.Set(window, parentWindow);
			var userControl = new TUserControl
			{
				DataContext = viewModel
			};
			window.Content = userControl;
			return window;
		}

		protected void Set(Window window, Window parentWindow)
		{
			Window = window;
			ParentWindow = parentWindow;
		}

		public ICommand CancelCommand { get { return new Command(Close); } }
		protected void Close()
		{
			Window.Close();
		}

		public ICommand CloseAndRefreshCommand { get { return new Command(CloseAndRefresh); } }
		protected void CloseAndRefresh()
		{
			if (IsParentWindowValid)
			{
				var viewModel = (ParentWindow.DataContext as MainViewModel);
				if (viewModel != null)
				{
					viewModel.SearchViewModel.RefreshAreaTypes();
					viewModel.SearchViewModel.RefreshStockTypeGroups();
					viewModel.SearchViewModel.RefreshList();
				}
			}
			Window.Close();
		}

		public bool IsParentWindowValid { get { return ParentWindow != null; } }
		public bool IsWindowValid { get { return Window != null; } }
		public Window ParentWindow { get; set; }
		public Window Window { get; set; }

		#region Inherited
		public event PropertyChangedEventHandler PropertyChanged;

		protected static string GetPropertyName<T>(Expression<Func<T>> action)
		{
			return ((MemberExpression)action.Body).Member.Name;
		}

		protected void OnPropertyChanged<T>(Expression<Func<T>> action)
		{
			OnPropertyChanged(GetPropertyName(action));
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		} 
		#endregion
	}
}
