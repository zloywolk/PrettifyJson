using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Model
{
    internal class Address : INotifyPropertyChanged
    {
        private string _city;
        private string _country;
        private int _zipcode;

        public string Country
        {
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Zipcode
        {
            get { return _zipcode; }
            set
            {
                if (value != _zipcode)
                {
                    _zipcode = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}