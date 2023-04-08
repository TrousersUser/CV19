using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters.Base
{
    internal abstract class MultiConverter : IMultiValueConverter
    {
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public virtual object[] ConvertBack(object value, Type[] types, object parameter, CultureInfo culture)
            => throw new NotSupportedException($"Обратное преобразование значения {nameof(value)} не поддерживается.");

    }
}
