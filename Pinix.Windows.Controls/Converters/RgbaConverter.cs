using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pinix.Windows.Controls.Converters
{
    public class RgbaConverter : AbstractConverter<RgbaConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value.ToString();

            var converter = new BrushConverter();
            if (!converter.IsValid(brush)) return null;

            var hex = int.Parse(brush.Substring(1), NumberStyles.HexNumber);
            if (hex < 0x01000000) hex = (hex << 8) | 0xff;

            var r = (int)(hex >> 24) & 0xff;
            var g = (int)(hex >> 16) & 0xff;
            var b = (int)(hex >> 8) & 0xff;
            var a = (int)(hex & 0xff);

            return $"{r}, {g}, {b}, {a}";
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
