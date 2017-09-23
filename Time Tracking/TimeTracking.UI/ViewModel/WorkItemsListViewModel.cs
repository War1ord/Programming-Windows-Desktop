using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace TimeTracking.UI.ViewModel
{
    public class WorkItemsListViewModel : ViewModelBase
    {
        public List<Models.WorkItem> List { get; set; }
        public Models.WorkItem Selected { get; set; }

        private string _statusMessage;

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                RaisePropertyChanged(() => StatusMessage);
            }
        }
    
    }
}