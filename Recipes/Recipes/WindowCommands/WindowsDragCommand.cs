using System;
using System.Windows;
using System.Windows.Input;

namespace Recipes.WindowCommands
{
	public class WindowsDragCommand : ICommand
	{
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged { add { } remove { } }

		public void Execute(object wnd)
		{
			if (!(wnd is Window)) return;
			((Window)wnd).DragMove();
		}
	}
}
