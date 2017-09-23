using System.Collections.Generic;

namespace Framework.Wpf.Mvvm.Core.ViewModels
{
	public class SummaryListViewModel : Base.Base
	{
		public IEnumerable<SummaryItemViewModel> List { get; set; }
	}
}