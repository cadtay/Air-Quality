using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Models.OpenAQ.Countries
{
    public class Results
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Locations { get; set; }

        public string Cities { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
