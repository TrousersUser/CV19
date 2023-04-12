using System;
using System.Windows.Data;
using System.Globalization;
using MapControl;
using System.Windows;
using CV19.Infrastructure.Converters.Base;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(PointToLocation))]
    [ValueConversion(typeof(Point),typeof(Location))]
    internal class PointToLocation : Converter
    {
        public override object Convert(object value, Type targetType, object param, CultureInfo culture)
        {
            if (!(value is Point point) || value == null) return null;
            return new Location(point.X,point.Y);
        }

        public override object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        {
            if (!(value is Location loc) || value == null) return null;
            return new Point(x: loc.Latitude, y: loc.Longitude);
        }
    }
}
