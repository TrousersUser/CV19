
using System.Collections.Generic;
using System.Windows;

namespace CV19.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }

        /// <summary>
        /// Point - structure what gives an opportunity to use X,Y points, containing any of numeric in int type information
        /// </summary>
        public Point Location { get; set; }
        public IEnumerable<ConfirmedCount> Confirmed { get; set; }

    }
}
