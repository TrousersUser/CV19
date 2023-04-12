
using CV19.Infrastructure.Converters.Base;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ToArray : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = new CompositeCollection();
            foreach (object item in values)
                collection.Add(item);
            return collection;
        }
    }
}
