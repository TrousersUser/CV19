using System.Globalization;
using System;
using System.Windows.Data;
using System.Windows.Markup;
using CV19.Infrastructure.Converters.Base;

namespace CV19.Infrastructure.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    [MarkupExtensionReturnType(typeof(Linear))]
    internal class Linear : Converter
    {
        /// <summary>
        /// Конвертер нацелен на реализацию линейного преобразования f(x) = k*x + b
        /// </summary>
        /// <param name="value">значение для конвертации</param>
        /// <param name="targetType"></param>
        /// <param name="param"></param>
        /// <param name="culture"></param>
        #region k, b : double - Коэфиценты, учавствующующий в преобразовании данных, через формулу линейной функции.
        [ConstructorArgument("_k")]
        public double k { get; set; } = 1;
        [ConstructorArgument("_b")]
        public double b { get; set; } = 0;
        #endregion
        public Linear() { }
        public Linear(double _k) => k = _k;
        public Linear(double _k, double _b) : this(_k) => b = _b;
        public override object Convert(object value, Type targetType, object param, CultureInfo culture)
        {
            if (value is null) return null;
            return (k * System.Convert.ToDouble(value, culture)) + b; // where "value" is X, K - ratio(соотношение);
        }
        public override object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        {
            if (value is null) return null;
            return (System.Convert.ToDouble(value, culture) - b) / k; // where "value" is Y, K - ratio(соотношение);
        }
    }
}
