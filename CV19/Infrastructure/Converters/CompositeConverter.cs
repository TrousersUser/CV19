using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(CompositeConverter))]
    internal class CompositeConverter : Converter
    {
        [ConstructorArgument("first")]
        public IValueConverter? first { get; set; }
        [ConstructorArgument("second")]
        public IValueConverter? second { get; set; }

        public CompositeConverter() { }
        public CompositeConverter(IValueConverter? first) => this.first = first;
        public CompositeConverter(IValueConverter? first, IValueConverter? second) : this(first)
            => this.second = second;

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
