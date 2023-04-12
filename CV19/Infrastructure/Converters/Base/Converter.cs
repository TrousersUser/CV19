using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters.Base
{
    internal abstract class Converter : MarkupExtension,IValueConverter
    {
        public override object ProvideValue(IServiceProvider provider) => this;
        public abstract object Convert(object value, Type targetType, object param, CultureInfo culture);
        public virtual object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        => throw new NotSupportedException("Изначальная реализация метода обратной конвертации не поддерживается;\nBase implementation for back convertation not supported.");
    }
}
