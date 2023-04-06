using System.Globalization;
using System;

namespace CV19.Infrastructure.Converters
{
    internal class Linear : Converter
    {
        /// <summary>
        /// Конвертер нацелен на реализацию линейного преобразования f(x) = k*x + b
        /// </summary>
        /// <param name="value">значение для конвертации</param>
        /// <param name="targetType"></param>
        /// <param name="param"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region k, b : double - Коэфиценты, учавствующующий в преобразовании данных, через формулу линейной функции.
        public double k { get; set; } = 1;
        public double b { get; set; } = 0; 
        #endregion
        public override object Convert(object value, Type targetType, object param, CultureInfo culture)
        {
            if (value is null) return null;
            return (k * System.Convert.ToDouble(value, culture)) + b; // where "value" is X, K - ratio
        }
        public override object ConvertBack(object value, Type targetType, object param, CultureInfo culture)
        {
            if (value is null) return null;
            return (System.Convert.ToDouble(value, culture) - b) / k; // where "value" is Y, K - ratio;
        }
    }
}
