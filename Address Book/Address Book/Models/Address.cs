using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Address_Book.Models
{
    public class Address : ViewModelBase
    {
        private Enums.AddressType _type;
        private string _extra;
        private string _streetNumber;
        private string _streetName;
        private string _postalCode;
        private string _suburb;
        private string _city;
        private string _province;

        public Address()
        {

        }

        public Address(Address address)
        {
            Province = address.Province;
            City = address.City;
            Suburb = address.Suburb;
            PostalCode = address.PostalCode;
            StreetName = address.StreetName;
            StreetNumber = address.StreetNumber;
            Extra = address.Extra;
        }

        public Enums.AddressType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged(() => Type);
                RaisePropertyChanged(() => Display);
            }
        }
        public string Province
        {
            get { return _province; }
            set
            {
                _province = value;
                RaisePropertyChanged(() => Province);
                RaisePropertyChanged(() => Display);
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);
                RaisePropertyChanged(() => Display);
            }
        }
        public string Suburb
        {
            get { return _suburb; }
            set
            {
                _suburb = value;
                RaisePropertyChanged(() => Suburb);
                RaisePropertyChanged(() => Display);
            }
        }
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                RaisePropertyChanged(() => PostalCode);
                RaisePropertyChanged(() => Display);
            }
        }
        public string StreetName
        {
            get { return _streetName; }
            set
            {
                _streetName = value;
                RaisePropertyChanged(() => StreetName);
                RaisePropertyChanged(() => Display);
            }
        }
        public string StreetNumber
        {
            get { return _streetNumber; }
            set
            {
                _streetNumber = value;
                RaisePropertyChanged(() => StreetNumber);
                RaisePropertyChanged(() => Display);
            }
        }
        public string Extra
        {
            get { return _extra; }
            set
            {
                _extra = value;
                RaisePropertyChanged(() => Extra);
                RaisePropertyChanged(() => Display);
            }
        }

        public string Display
        {
            get
            {
                return string.Format("{0}: {1}, {2}, \r\n{3} {4} {5},  {6}{7}"
                    , Type, City, Province, StreetNumber, StreetName, Suburb, PostalCode
                    , !string.IsNullOrWhiteSpace(Extra) ? System.Environment.NewLine + Extra : string.Empty);
            }
        }

    }
}