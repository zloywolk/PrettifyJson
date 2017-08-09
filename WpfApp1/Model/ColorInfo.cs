using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WpfApp1.Model
{
    public class ColorInfo : INotifyPropertyChanged
    {
        private Color _color;

        public Color Color
        {
            get { return _color; }
            set
            {
                if (value != _color)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }

        private Brush _brush;

        public Brush Brush
        {
            get { return _brush; }
            set
            {
                if (value != _brush)
                {
                    _brush = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}