﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Pinix.Windows.Controls.Converters
{
    public abstract class AbstractConverter<T> 
        : MarkupExtension, IValueConverter, IMultiValueConverter where T : class, new()
    {
        private static T _converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter = _converter ?? new T();
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
    }
}
