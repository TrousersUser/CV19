using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _Location;
        public override Point Location
        {
            get
            {
                if (_Location != null)
                    return (Point)_Location;

               else if (_Location == null)
                    return default(Point); // Возвращается экземпяр значимого типа - структуры, что,
                                           // благодаря оператору default, получает инициализацию для каждого поля/свойства,
                                           // с помощью значения по умолчания, соответствующему типу для элемента.
                double average_x = ProvinceCounts.Average(s => s.Location.X);
                double average_y = ProvinceCounts.Average(s => s.Location.Y);
                return (Point)(_Location = new Point(average_x, average_y));
            }
            set => _Location = value;
        }
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }
}
