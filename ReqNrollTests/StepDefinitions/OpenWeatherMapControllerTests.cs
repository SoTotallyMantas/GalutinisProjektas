using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalutinisProjektas.Server.Controllers;
using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Models.AirPollutionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Interfaces;

namespace ReqNrollQuickstart.Specs.StepDefinitions
{
    [TestFixture]
    [Binding]
    public class OpenWeatherMapControllerStepDefinitions
    {
        private OpenWeatherMapController _controller;
        private Mock<ILogger<OpenWeatherMapController>> _loggerMock;
        private Mock<IOpenWeatherMapService> _serviceMock;
        private double _latitude;
        private double _longitude;
        private ActionResult<AirPollutionResponse>? _result;

        public OpenWeatherMapControllerStepDefinitions()
        {
            _loggerMock = new Mock<ILogger<OpenWeatherMapController>>();
            _serviceMock = new Mock<IOpenWeatherMapService>();
            _controller = new OpenWeatherMapController(_loggerMock.Object, _serviceMock.Object);
            _result = null;
        }

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<OpenWeatherMapController>>();
            _serviceMock = new Mock<IOpenWeatherMapService>();
            _controller = new OpenWeatherMapController(_loggerMock.Object, _serviceMock.Object);
            _result = null;
        }

        [Given("I have the latitude (.*) and longitude (.*)")]
        public void GivenIHaveTheLatitudeAndLongitude(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        [When("I request air pollution data")]
        public async Task WhenIRequestAirPollutionData()
        {
            _result = await _controller.Get(_latitude, _longitude);
        }

        [Then("the response should be successful and contain air pollution details")]
        public void ThenTheResponseShouldBeSuccessfulAndContainAirPollutionDetails()
        {
            Assert.That(_result, Is.InstanceOf<ActionResult<AirPollutionResponse>>());
            if (_result?.Result is OkObjectResult okResult)
            {
                Assert.That(okResult.StatusCode, Is.EqualTo(200));

                var airPollutionResponse = okResult.Value as AirPollutionResponse;
                Assert.IsNotNull(airPollutionResponse);
                Assert.That(airPollutionResponse.Coord.Latitude, Is.EqualTo(_latitude));
                Assert.That(airPollutionResponse.Coord.Longitude, Is.EqualTo(_longitude));
                Assert.IsNotEmpty(airPollutionResponse.List);
            }
        }

        [Then("the response should indicate an invalid request")]
        public void ThenTheResponseShouldIndicateAnInvalidRequest()
        {
            Assert.That(_result, Is.InstanceOf<ActionResult<AirPollutionResponse>>());
            if (_result?.Result is BadRequestObjectResult badRequestResult)
            {
                Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
                Assert.That(badRequestResult.Value, Is.EqualTo("Invalid coordinates"));
            }
        }

        
        [Test]
        public async Task Get_AirPollutionData_ForValidCoordinates()
        {
            GivenIHaveTheLatitudeAndLongitude(34.05, -118.25);

            _serviceMock.Setup(s => s.GetAirPollutionDataAsync(It.IsAny<double>(), It.IsAny<double>()))
                        .ReturnsAsync(new ServiceResponse<AirPollutionResponse>
                        {
                            Data = new AirPollutionResponse
                            {
                                Coord = new AirPollutionCoord { Latitude = 34.05, Longitude = -118.25 },
                                List = new List<WeatherData>()
                            },
                            StatusCode = 201
                        });

            await WhenIRequestAirPollutionData();

            ThenTheResponseShouldBeSuccessfulAndContainAirPollutionDetails();
        }

        [Test]
        public async Task Get_AirPollutionData_ForInvalidCoordinates()
        {
            GivenIHaveTheLatitudeAndLongitude(999, -999);
            
            _serviceMock.Setup(s => s.GetAirPollutionDataAsync(It.IsAny<double>(), It.IsAny<double>()))
                        .ReturnsAsync(new ServiceResponse<AirPollutionResponse>
                        {
                            StatusCode = 400,
                            ErrorMessage = "Invalid coordinates"
                        });

            await WhenIRequestAirPollutionData();

            ThenTheResponseShouldIndicateAnInvalidRequest();
        }

    }
}
