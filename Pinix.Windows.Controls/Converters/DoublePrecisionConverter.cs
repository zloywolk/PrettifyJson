using System;
using System.Globalization;

namespace Pinix.Windows.Controls.Converters
{
    class DoublePrecisionConverter : AbstractConverter<DoublePrecisionConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var precision = (int?) parameter ?? 0;
            var d = (double?) value ?? 0.0;

            return $"{Math.Round(d, precision)}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 1) return null;
            var precision = values.Length < 2 ? 0 : ((int?)values[1] ?? 0);
            var d = (double?)values[0] ?? 0.0;

            return $"{Math.Round(d, precision)}";
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
