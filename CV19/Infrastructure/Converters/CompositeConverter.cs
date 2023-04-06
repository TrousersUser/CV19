using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class CompositeConverter : Converter
    {
        public IValueConverter? first { get; set; }
        public IValueConverter? second { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object firstResult = first?.Convert(value, targetType, parameter, culture) ?? value;
            object secondResult = second?.Convert(firstResult, targetType, parameter, culture) ?? firstResult;

            return secondResult;

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object secondResult = second?.ConvertBack(value, targetType, parameter, culture) ?? value;
            object firstResult = first?.ConvertBack(secondResult, targetType, parameter, culture) ?? secondResult;

            return firstResult;
        }
    }
}
