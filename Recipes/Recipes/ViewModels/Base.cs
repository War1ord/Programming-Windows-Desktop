using System.Windows;
using GalaSoft.MvvmLight;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.ViewModels
{
	public abstract class Base : ViewModelBase
	{
		#region Fields

		private static string _userFriendlyError;
		private string _userMessage;

		#endregion

		#region Constructors

		protected Base()
		{
			UserFriendlyError = string.Empty;
            UserMessage = string.Empty;
		}

		#endregion

		#region Properties

		protected static string UserFriendlyError
        {
	        get { return _userFriendlyError; }
	        set
	        {
				_userFriendlyError = value;
				if (!string.IsNullOrWhiteSpace(value))
					MessageBox.Show(value, "Error!", MessageBoxButton.OK);
	        }
        }
		protected string UserMessage
		{
			get { return _userMessage; }
			set
			{
				_userMessage = value;
				if (!string.IsNullOrWhiteSpace(value))
					MessageBox.Show(value, "Information!", MessageBoxButton.OK);
			}
		}

		#endregion

		#region Helpers

		protected bool IsValid(int index, int count)
		{
			return index >= 0 && index < count;
		}
		protected bool IsValid(IEnumerable<Recipe> collection)
		{
			return collection != null && collection.Any();
		}
		protected static bool Log(Exception exception)
		{
			UserFriendlyError = Business.Errors.Log(exception);
			return false;
		}

		#endregion
	}
}