using Air_Quality.Builders;
using Air_Quality.Directors;
using Air_Quality.Directors.AirQuality.Get.AllCities;
using Air_Quality.Directors.AirQualityApi.Countries.Get.GetAllCountries;
using Air_Quality.Directors.OpenAQ.AirQuality.Get;
using Air_Quality.Directors.OpenAQ.AirQuality.Get.GetAirQualityByLocationId;
using Air_Quality.Models.AirQuality.Cities;
using Air_Quality.Models.OpenAQ.AirQuality;
using Air_Quality.Models.OpenAQ.Countries;
using Air_Quality.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality
{
    public class Startup
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();

            webHostEnvironment = env;

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<UrlOptions>(Configuration.GetSection("ApiOptions"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddHttpClient();

            services.AddTransient<IDirector<GetAllCitiesParameters, Task<CityModel>>, GetAllCities>();
            services.AddTransient<IDirector<GetAllCountriesParameters, Task<CountryModel>>, GetAllCountries>();
            services.AddTransient<IDirector<GetAirQualityParameters, Task<AirQualityModel>>, GetAirQuality>();
            services.AddTransient<IDirector<GetAirQualityByLocationIdParameters, Task<AirQualityModel>>, GetAirQualityByLocationId>();

            services.AddScoped<IQueryBuilder, QueryBuilder>();

            // Application Insights
            services.AddApplicationInsightsTelemetry();

            services.AddSingleton<IMemoryCache, MemoryCache>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
