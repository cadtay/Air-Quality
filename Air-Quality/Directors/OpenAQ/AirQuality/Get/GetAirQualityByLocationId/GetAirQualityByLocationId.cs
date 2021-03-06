using Air_Quality.Builders;
using Air_Quality.Clients;
using Air_Quality.Models.OpenAQ.AirQuality;
using Air_Quality.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Air_Quality.Directors.OpenAQ.AirQuality.Get.GetAirQualityByLocationId
{
    public class GetAirQualityByLocationId : BaseHttpClient, IDirector<GetAirQualityByLocationIdParameters, Task<AirQualityModel>>
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IOptions<UrlOptions> _options;
        private readonly IQueryBuilder _queryBuilder;
        private readonly ILogger<GetAirQualityByLocationId> _logger;
        public GetAirQualityByLocationId(IHttpClientFactory httpClient, IOptions<UrlOptions> options, IQueryBuilder queryBuilder, ILogger<GetAirQualityByLocationId> logger) : base(httpClient, logger)
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
        public async Task<AirQualityModel> Execute(GetAirQualityByLocationIdParameters parameters)
        {
            try
            {
                var routePath = $"latest/{parameters.LocationId}";
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

                var resp = await GetAsync<AirQualityModel>(routePath);

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
