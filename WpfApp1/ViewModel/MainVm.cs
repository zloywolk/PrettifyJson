using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.Commands;
using WpfApp1.Data;
using WpfApp1.Model;
using Newtonsoft.Json;

using System.IO;
using WpfApp1.Servcies;

namespace WpfApp1.ViewModel
{
    class MainVm : AbstractViewModel
    {
        public object JsonData
        {
            get => _jsonData;
            set
            {
                _jsonData = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addUserCommand;

        public ICommand AddUserCommand => _addUserCommand = _addUserCommand ?? new RelayCommand(() =>
        {
            try
            { 
                JsonData = _ioService.Read();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        });

        private ObservableCollection<ColorInfo> _brushes;
        private object _jsonData;
        private IIoService _ioService;

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
            _ioService = new Win32IoService();
            Brushes = new ObservableCollection<ColorInfo>();
            foreach (var pi in typeof(System.Windows.Media.Brushes).GetProperties())
            {
                var value = (SolidColorBrush)pi.GetValue(null);
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
