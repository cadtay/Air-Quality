using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Models.AirQuality.Cities
{
    public class Results
    {
        public string Country { get; set; }

        public string City { get; set; }

        public int  Locations { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

