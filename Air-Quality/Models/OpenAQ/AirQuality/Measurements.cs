using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Models.OpenAQ.AirQuality
{
    public class Measurements
    {
        public string Parameter { get; set; }

        public double Value { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Unit { get; set; }
    }
}