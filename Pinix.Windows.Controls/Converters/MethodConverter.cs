using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Pinix.Windows.Controls.Converters
{
    public class MethodConverter : AbstractConverter<MethodConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null) return null;
            var method = parameter as string;
            if (method == null) return null;

            switch (((JToken)value).Type)
            {
                case JTokenType.Array:
                    return value;
                case JTokenType.Object:
                case  JTokenType.Raw:
                    return GetTokens(value, method);
                case JTokenType.Property:
                    return GetTokens(value, method)?.First().Children();
                default:
                    return value;
            }
        }

        private static IEnumerable<JToken> GetTokens(object value, string method)
        {
            var mi = value.GetType().GetMethod(method, new Type[0]);
            var jTokens = mi.Invoke(value, new object[0]) as IEnumerable<JToken>;

            return jTokens;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}