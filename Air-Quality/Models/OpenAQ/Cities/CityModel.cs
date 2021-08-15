using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Models.AirQuality.Cities
{
    public class CityModel
    {
        public BaseModel Meta { get; set; }

        public List<Results> Results { get; set; }
    }
}
