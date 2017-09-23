using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Address_Book.ViewModel
{
    public class EnumsViewModel : ViewModelBase
    {
        public List<Enums.SortBy> SortByList { get { return Helpers.EnumHelpers.ConvertEnumToList<Enums.SortBy>(); } }
        public List<Enums.AddressType> Types { get { return Helpers.EnumHelpers.ConvertEnumToList<Enums.AddressType>(); } }
    }
}