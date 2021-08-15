using Air_Quality.Builders;
using Air_Quality.Clients;
using Air_Quality.Models.AirQuality.Cities;
using Air_Quality.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Air_Quality.Directors.AirQuality.Get.AllCities
{
    public class GetAllCities : BaseHttpClient, IDirector<GetAllCitiesParameters, Task<CityModel>>
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IOptions<UrlOptions> _options;
        private readonly IQueryBuilder _queryBuilder;
        private readonly ILogger<GetAllCities> _logger;

        public GetAllCities(IHttpClientFactory httpClient, IOptions<UrlOptions> options, IQueryBuilder queryBuilder, ILogger<GetAllCities> logger) : base(httpClient, logger)
        {
            _httpClient = httpClient;
            _options = options;
            _queryBuilder = queryBuilder;
            _logger = logger;
        }
        protected override string BaseUrl
        {
            get => _options.Value.OpenAqApi;
        }

        public async Task<CityModel> Execute(GetAllCitiesParameters parameters)
        {
            try
            {
                var routePath = "cities";
                var query = "";

                if (parameters != null)
                {

                    query = _queryBuilder
                    .Add("limit", parameters.Limit ?? "")
                    .Add("page", parameters.Page ?? "")
                    .Add("offset", parameters.Offset ?? "")
                    .Add("sort", parameters.SortBy.ToString() ?? "")
                    .Add("country_id", parameters.CountryId)
                    .Add<List<string>>("country", parameters.Country)
                    .Add<List<string>>("city", parameters.City)
                    .Add("order_by", parameters.OrderBy.ToString() ?? "")
                    .Add("entity", parameters.Entity)
                    .Build();
                }

                if (!string.IsNullOrEmpty(query))
                    routePath += query;

                var resp = await GetAsync<CityModel>(routePath);

                return resp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
