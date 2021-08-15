using Air_Quality.Directors;
using Air_Quality.Directors.AirQualityApi.Countries.Get.GetAllCountries;
using Air_Quality.Models.OpenAQ.Countries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Air_Quality_Tests.ControllerTests.CountriesController
{
    public class CountriesControllerTests
    {
        private readonly IDirector<GetAllCountriesParameters, Task<CountryModel>> _getAllCountries;
        private readonly Mock<ILogger<Air_Quality.Controllers.v1.CountriesController>> _logger;

        private readonly Air_Quality.Controllers.v1.CountriesController _controller;

        public CountriesControllerTests()
        {
            _getAllCountries = Mock.Of<IDirector<GetAllCountriesParameters, Task<CountryModel>>>();
            _logger = new Mock<ILogger<Air_Quality.Controllers.v1.CountriesController>>();

            _controller = new Air_Quality.Controllers.v1.CountriesController(_getAllCountries, _logger.Object);
        }

        [Fact]
        public void CreateApplicationController()
        {
            Assert.NotNull(_controller);
        }

        [Fact]
        public async Task GiveCountriesController_WhenGettingAllCountries_ThenReturnOkResults()
        {
            var expectedResult = new CountryModel
            {
                Results = new List<Results>
                {
                    new Results
                    {
                        Code = "AE"
                    } 
                }
            };

            Mock.Get(_getAllCountries).Setup(x => x.Execute(It.IsAny<GetAllCountriesParameters>())).ReturnsAsync(expectedResult);

            var response = await _controller.GetAllCountries(new GetAllCountriesParameters { }) as OkObjectResult;

            var countries = response?.Value as CountryModel;

            Assert.NotNull(response);
            Assert.NotNull(countries);
            Assert.Equal(expectedResult.Results.First().Code, countries.Results.First().Code);

            Mock.Get(_getAllCountries).Verify(x => x.Execute(It.IsAny<GetAllCountriesParameters>()), Times.Once);
        }

        [Fact]
        public async Task GiveCountriesController_WhenGettingAllCountries_ThenReturnNotFound()
        {
            CountryModel expectedResult = null;

            Mock.Get(_getAllCountries).Setup(x => x.Execute(It.IsAny<GetAllCountriesParameters>())).ReturnsAsync(expectedResult);

            var response = await _controller.GetAllCountries(new GetAllCountriesParameters{ }) as NotFoundObjectResult;

            var countries = response?.Value as CountryModel;

            Assert.Null(response);
            Assert.Null(countries);

            Mock.Get(_getAllCountries).Verify(x => x.Execute(It.IsAny<GetAllCountriesParameters>()), Times.Once);
        }
    }
}
