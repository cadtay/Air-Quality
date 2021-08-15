using Air_Quality.Builders;
using Air_Quality.Clients;
using Air_Quality.Models.OpenAQ.Countries;
using Air_Quality.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Air_Quality.CacheKeys;

namespace Air_Quality.Directors.AirQualityApi.Countries.Get.GetAllCountries
{
    public class GetAllCountries : BaseHttpClient, IDirector<GetAllCountriesParameters, Task<CountryModel>>
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IOptions<UrlOptions> _options;
        private readonly IQueryBuilder _queryBuilder;
        private readonly ILogger<GetAllCountries> _logger;
        private readonly IMemoryCache _memoryCache;

        public GetAllCountries(IHttpClientFactory httpClient, IOptions<UrlOptions> options, IQueryBuilder queryBuilder, ILogger<GetAllCountries> logger,
            IMemoryCache memoryCache) : base(httpClient, logger)
        {
            _httpClient = httpClient;
            _options = options;
            _queryBuilder = queryBuilder;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        protected override string BaseUrl
        {
            get => _options.Value.OpenAqApi;
        }

        public async Task<CountryModel> Execute(GetAllCountriesParameters parameters)
        {
            try
            {
                CountryModel countries;

                if (!_memoryCache.TryGetValue(CacheKeys.CacheKeys.Countries, out countries))
                {
                    var routePath = "countries";
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
                        .Build();
                    }

                    if (!string.IsNullOrEmpty(query))
                        routePath += query;

                    var resp = await GetAsync<CountryModel>(routePath);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                    _memoryCache.Set(CacheKeys.CacheKeys.Countries, resp, cacheEntryOptions);

                    return resp;
                } 
                else
                {
                    return countries;
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
