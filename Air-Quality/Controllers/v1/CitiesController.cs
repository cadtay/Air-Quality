using Air_Quality.Directors;
using Air_Quality.Directors.AirQuality.Get.AllCities;
using Air_Quality.Models.AirQuality.Cities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Air_Quality.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ILogger<CitiesController> _logger;

        private readonly IDirector<GetAllCitiesParameters, Task<CityModel>> _getAllCities;

        public CitiesController(ILogger<CitiesController> logger, IDirector<GetAllCitiesParameters, Task<CityModel>> getAllCities)
        {
            _logger = logger;
            _getAllCities = getAllCities;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery]GetAllCitiesParameters parameters)
        {
            try
            {

                var cities = await _getAllCities.Execute(parameters);

                if (cities == null)
                    return NotFound();

                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
