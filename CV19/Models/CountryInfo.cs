using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        //private Point? _Location;
        //public override Point Location
        //{
        //    get
        //    {
        //        if (_Location != null)
        //            return (Point)_Location;

        //        else if (_Location is null)
        //            return default(Point); // Возвращается экземпяр значимого типа - структуры, что,
        //                                   // благодаря оператору default, получает инициализацию для каждого поля/свойства,
        //                                   // с помощью значения по умолчания, соответствующему типу для элемента.
        //        var average_x = ProvinceCounts.Average(s => s.Location.X);
        //        var average_y = ProvinceCounts.Average(s => s.Location.Y);
        //        return (Point)(_Location = new Point(average_x, average_y));
        //    }
        //    set => _Location = value;
        //}

        private Point? _Location;

        public override Point Location
        {
            get
            {
                if (_Location != null)
                    return (Point)_Location;

                if (ProvinceCounts is null) return default(Point);
                // Возвращается экземпяр значимого типа - структуры, что,
                // благодаря оператору default, получает инициализацию для каждого поля/свойства,
                // с помощью значения по умолчания, соответствующему типу для элемента.

                var average_x = ProvinceCounts.Average(p => p.Location.X);
                var average_y = ProvinceCounts.Average(p => p.Location.Y);

                return (Point)(_Location = new Point(average_x, average_y));
            }
            set => _Location = value;
        }
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }

        private IEnumerable<ConfirmedCount> _Confirmed;
        public override IEnumerable<ConfirmedCount> Confirmed
        {
            get
            {
                if (_Confirmed != null) return _Confirmed;

                int pointsCount = ProvinceCounts.FirstOrDefault()?.Confirmed.Count() ?? 0; // количество подтвержденных мест, где произошло заражение.
                if (pointsCount == 0) return Enumerable.Empty<ConfirmedCount>();

                ConfirmedCount[] points = new ConfirmedCount[pointsCount];
                ConfirmedCount[][] provincePoints = ProvinceCounts
                    .Select(province => province.Confirmed.ToArray())
                    .ToArray();

                foreach (ConfirmedCount[] province in provincePoints)
                    for (int i = 0; i < pointsCount; i++)
                    {
                        if (points[i].Date == default)
                            points[i] = province[i];
                        else
                            points[i].Count += province[i].Count;
                    }
                return _Confirmed = points;
            }
            set => _Confirmed = value;
        }
    }
}



