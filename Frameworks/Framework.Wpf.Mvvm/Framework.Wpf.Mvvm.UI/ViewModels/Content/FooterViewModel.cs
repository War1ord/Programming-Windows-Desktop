using System;
using Framework.Wpf.Mvvm.Core.ViewModels;
using Framework.Wpf.Mvvm.Core.ViewModels.Base;

namespace Framework.Wpf.Mvvm.UI.ViewModels.Content
{
	public class FooterViewModel : ContentBase
	{
		public FooterViewModel()
		{
			FooterDescription = "Wpf Mvvm Framework Application " + DateTime.Now.ToString("f");
		}

		public string FooterDescription { get; set; }
	}
}