﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Recipes.WindowCommands
{
	public class WindowMinimizeCommand : ICommand
	{
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged { add { } remove { } }

		public void Execute(object wnd)
		{
			if (!(wnd is Window)) return;
			var window = ((Window) wnd);
			window.WindowState = window.WindowState != WindowState.Minimized
				                     ? WindowState.Minimized
				                     : WindowState.Normal;
		}
	}
}
