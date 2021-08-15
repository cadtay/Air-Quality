using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Air_Quality.Clients
{
    public abstract class BaseHttpClient
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<BaseHttpClient> _logger;

        public BaseHttpClient(IHttpClientFactory httpClient, ILogger<BaseHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        protected async Task<TOut> GetAsync<TOut>(string routePath)
        {
            try
            {
                var client = _httpClient.CreateClient();

                var resp = await client.GetAsync($"{BaseUrl}/{routePath}");

                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStringAsync();

                var deserialized = JsonConvert.DeserializeObject<TOut>(content);

                return deserialized;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        protected async Task<TOut> GetAsync<TOut>(string routePath, CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClient.CreateClient();

                var resp = await client.GetAsync($"{BaseUrl}/{routePath}", cancellationToken);

                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStringAsync();

                var deserialized = JsonConvert.DeserializeObject<TOut>(content);

                return deserialized;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        protected abstract string BaseUrl { get; }
    }
}
