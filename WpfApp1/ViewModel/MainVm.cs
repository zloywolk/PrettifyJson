using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Commands;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    class MainVm : AbstractViewModel
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                if (value != _user)
                {
                    _user = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                if (value != _users)
                {
                    _users = value;
                    OnPropertyChanged();
                }
            }
        }

        private ICommand _addUserCommand;

        public ICommand AddUserCommand => _addUserCommand = _addUserCommand ?? new RelayCommand(() =>
        {
            var users = Users;
            users.Add(UserSource.GetUser());
            Users = new ObservableCollection<User>(users);
        });

        private ObservableCollection<ColorInfo> _brushes;

        public ObservableCollection<ColorInfo> Brushes
        {
            get { return _brushes; }
            set
            {
                if (value != _brushes)
                {
                    _brushes = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainVm()
        {
            User = Data.UserSource.GetUser();
            Users = new ObservableCollection<User>();
            Brushes = new ObservableCollection<ColorInfo>();
            foreach (var pi in typeof (System.Windows.Media.Brushes).GetProperties())
            {
                var value = (SolidColorBrush) pi.GetValue(null);
                if (value != null)
                {
                    Brushes.Add(new ColorInfo
                    {
                        Color = value.Color,
                        Brush = value,
                        Name = pi.Name
                    });
                }
            }
        }
    }
}
