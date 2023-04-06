using System;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Data;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
    [ValueConversion(typeof(double),typeof(double))]
    internal class Ratio : Converter
    {
        [ConstructorArgument("y")]
        // атрибут указывает на то, что свойство может быть инициализировано с помощью параметра из конструктора.
        public double y { get; set; } = 1;
        public Ratio() { }
        public Ratio(double y) => this.y = y;
       public override object Convert(object value, Type targetType, object parameter, CultureInfo choosenCulture)
       {
            if (value == null) return null;
            return System.Convert.ToDouble(value) * y;
       }
       public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo choosenCulture)
       {
            if (value == null) return null;
            return System.Convert.ToDouble(value) / y;
       }
    }
}