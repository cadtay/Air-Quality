using Air_Quality.Directors;
using Air_Quality.Directors.AirQualityApi.Countries.Get.GetAllCountries;
using Air_Quality.Models.OpenAQ.Countries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CountriesController : Controller
    {
        private readonly IDirector<GetAllCountriesParameters, Task<CountryModel>> _getAllCountries;
        private readonly ILogger<CountriesController> _logger;
        public CountriesController(IDirector<GetAllCountriesParameters, Task<CountryModel>> getAllCities, ILogger<CountriesController> logger)
        {
            _getAllCountries = getAllCities;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries([FromQuery]GetAllCountriesParameters parameters)
        {
            try
            {
                var countries = await _getAllCountries.Execute(parameters);

                if (countries == null)
                    return NotFound();

                return Ok(countries);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
