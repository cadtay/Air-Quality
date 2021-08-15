using Air_Quality.Directors;
using Air_Quality.Directors.AirQuality.Get.AllCities;
using Air_Quality.Models.AirQuality.Cities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Air_Quality_Tests.ControllerTests.CitiesController
{
    public class CitiesControllerTests
    {
        private readonly Mock<ILogger<Air_Quality.Controllers.v1.CitiesController>> _logger;
        private readonly IDirector<GetAllCitiesParameters, Task<CityModel>> _getAllCities;

        private readonly Air_Quality.Controllers.v1.CitiesController _controller;
        public CitiesControllerTests()
        {
            _getAllCities = Mock.Of<IDirector<GetAllCitiesParameters, Task<CityModel>>>();
            _logger = new Mock<ILogger<Air_Quality.Controllers.v1.CitiesController>>();

            _controller = new Air_Quality.Controllers.v1.CitiesController(_logger.Object, _getAllCities);
        }

        [Fact]
        public void CreateApplicationController()
        {
            Assert.NotNull(_controller);
        }

        [Fact]
        public async Task GivenCitiesController_WhenGettingAllCities_ThenReturnOkResults()
        {
            var expectedResult = new CityModel
            {
                Results = new List<Results>
                {
                    new Results
                    {
                        City = "Test",
                    }
                }
            };

            Mock.Get(_getAllCities).Setup(x => x.Execute(It.IsAny<GetAllCitiesParameters>())).ReturnsAsync(expectedResult);

            var response = await _controller.GetAllCities(new GetAllCitiesParameters { }) as OkObjectResult;

            var cities = response?.Value as CityModel;

            Assert.NotNull(response);
            Assert.NotNull(cities);
            Assert.Equal(expectedResult.Results.First().City, cities.Results.First().City);

            Mock.Get(_getAllCities).Verify(x => x.Execute(It.IsAny<GetAllCitiesParameters>()), Times.Once);
        }
    }
}
