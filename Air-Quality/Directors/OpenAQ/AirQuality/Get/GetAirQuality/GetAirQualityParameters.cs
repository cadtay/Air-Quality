using Air_Quality.Enums;
using Air_Quality.Enums.Cities;
using Air_Quality.Models.OpenAQ.AirQuality;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Directors.OpenAQ.AirQuality.Get
{
    public class GetAirQualityParameters : IDirectorParameters<Task<AirQualityModel>>
    {
        [FromQuery(Name = "limit")]
        public string Limit { get; set; }

        [FromQuery(Name = "page")]
        public string Page { get; set; }

        [FromQuery(Name = "offset")]
        public string Offset { get; set; }

        [FromQuery(Name = "sort")]
        public SortBy? SortBy { get; set; }

        [FromQuery(Name = "country_id")]
        public string CountryId { get; set; }

        [FromQuery(Name = "country")]
        public List<string> Country { get; set; }

        [FromQuery(Name = "city")]
        public List<string> City { get; set; }

        [FromQuery(Name = "order_by")]
        public OrderBy? OrderBy { get; set; }

        [FromQuery(Name = "entity")]
        public string Entity { get; set; }
    }
}
