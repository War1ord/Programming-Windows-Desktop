using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Framework.Wpf.Mvvm.Core.ViewModels.Base
{
	public abstract class Base : INotifyPropertyChanged
	{
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
	}
}
