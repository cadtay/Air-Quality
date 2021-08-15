using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Models.OpenAQ.AirQuality
{
    public class Results
    {
        public string Location { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public Coordinates Coordinates { get; set; }

        public List<Measurements> Measurements { get; set; }
    }
}
