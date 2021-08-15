using Air_Quality.Directors;
using Air_Quality.Directors.OpenAQ.AirQuality.Get;
using Air_Quality.Directors.OpenAQ.AirQuality.Get.GetAirQualityByLocationId;
using Air_Quality.Models.OpenAQ.AirQuality;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Air_Quality.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AirQualityController : Controller
    {
        private readonly IDirector<GetAirQualityParameters, Task<AirQualityModel>> _getAirQuality;
        private readonly IDirector<GetAirQualityByLocationIdParameters, Task<AirQualityModel>> _getAirQualityByLocationId;
        private readonly ILogger<AirQualityController> _logger;
        public AirQualityController(IDirector<GetAirQualityParameters, Task<AirQualityModel>>  getAirQuality,
            IDirector<GetAirQualityByLocationIdParameters, Task<AirQualityModel>> getAirQualityByLocationId, ILogger<AirQualityController> logger)
        {
            _getAirQuality = getAirQuality;
            _getAirQualityByLocationId = getAirQualityByLocationId;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAirQuality([FromQuery]GetAirQualityParameters parameters)
        {
            try
            {
                var airQuality = await _getAirQuality.Execute(parameters);

                if (airQuality == null)
                    return NotFound();

                return Ok(airQuality);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{locationId}")]
        public async Task<IActionResult> GetAirQualityByLocationId([FromQuery] GetAirQualityByLocationIdParameters parameters, [FromRoute] string locationId)
        {
            try
            {
                if (string.IsNullOrEmpty(locationId))
                    return BadRequest();

                parameters.LocationId = locationId;

                var airQuality = await _getAirQualityByLocationId.Execute(parameters);

                if (airQuality == null)
                    return NotFound();

                return Ok(airQuality);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
