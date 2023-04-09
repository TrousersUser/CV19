using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    [MarkupExtensionReturnType(typeof(Converter))]
    internal class Add : Converter
    {
        [ConstructorArgument("_b")]
        public double b { get; set; } = 1;
        public Add(double _b) => b = _b;
        public Add() { }
        public override object Convert(object value, Type targetType, object param, CultureInfo culture)
        {
            if (value is null) return null;
            return System.Convert.ToDouble(value,culture) + b;
        }
        public override object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        {
            if (value is null) return null;
            return System.Convert.ToDouble(value, culture) - b;
        }
    }
}
