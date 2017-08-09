using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Model
{
    class User : INotifyPropertyChanged
    {
        private int _age;
        private string _name;
        private Address _address;
        private ObservableCollection<int> _randoms;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (value != _age)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ObservableCollection<int> Randoms
        {
            get { return _randoms; }
            set
            {
                if (value != _randoms)
                {
                    _randoms = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Address> _addresses;

        public ObservableCollection<Address> Addresses
        {
            get { return _addresses; }
            set
            {
                if (value != _addresses)
                {
                    _addresses = value;
                    OnPropertyChanged();
                }
            }
        }

        public Address Address
        {
            get { return _address; }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnPropertyChanged();
                }
            }
        }

        private double[] _doubleRandoms;

        public double[] DoubleRandoms
        {
            get { return _doubleRandoms; }
            set
            {
                if (value != _doubleRandoms)
                {
                    _doubleRandoms = value;
                    OnPropertyChanged();
                }
            }
        }

        private object _nullValue;

        public object NullValue
        {
            get { return _nullValue; }
            set
            {
                if (value != _nullValue)
                {
                    _nullValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (value != _isAdmin)
                {
                    _isAdmin = value;
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