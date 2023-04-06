using System;
using System.Globalization;
using System.Windows;

namespace CV19.Infrastructure.Converters
{
    internal class LocationPointToStr : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (!(value is Point point)) throw new ArgumentException("Конвертер нацелен на преобразования экземпляра класс Point (System.Windows;) в String(System)");

            return $"lon:{point.Y:F1};lat:{point.X:F1}";
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (!(value is string str)) return null;
            string[] strComponents = str.Split(';');

            string longStr = strComponents[0].Split(':')[1];
            string latStr = strComponents[1].Split(':')[1] ;

            return new Point(double.Parse(latStr), double.Parse(longStr));
        }
    }
}
