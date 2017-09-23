using System;
using System.Windows.Input;

namespace Framework.Wpf.Mvvm.Core.Commands
{
	public class Command : ICommand
	{
		private readonly Action _execute;
		private Func<bool> _canexeute;

		public Command(Action execute, Func<bool> canexecute = null)
		{
			_execute = execute;
			_canexeute = canexecute;
		}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public void Execute(object parameter)
		{
			_execute();
		}
		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <returns>
		/// true if this command can be executed; otherwise, false.
		/// </returns>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public bool CanExecute(object parameter)
		{
			return _canexeute == null || _canexeute();
		}
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}