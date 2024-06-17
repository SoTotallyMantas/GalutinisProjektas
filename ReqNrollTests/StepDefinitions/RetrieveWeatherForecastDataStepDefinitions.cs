using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using GalutinisProjektas.Server.Controllers;
using GalutinisProjektas.Server.Models;

namespace ReqNrollTests.StepDefinitions
{
    [TestFixture]
    [Binding]
    public class RetrieveWeatherForecastDataStepDefinitions
    {
        private WeatherForecastController _controller;
        private Mock<ILogger<WeatherForecastController>> _loggerMock;
        private IActionResult? _result;

        public RetrieveWeatherForecastDataStepDefinitions()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(_loggerMock.Object);
            _result = null;
        }

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(_loggerMock.Object);
            _result = null;
        }

        [Given("the weather forecast endpoint is available")]
        public void GivenTheWeatherForecastEndpointIsAvailable()
        {
            // Initialization if needed
        }

        [When("I request weather forecast data")]
        public void WhenIRequestWeatherForecastData()
        {
            _result = _controller.Get();
        }

        [Then("the response should be successful and contain weather forecast details")]
        public void ThenTheResponseShouldBeSuccessfulAndContainWeatherForecastDetails()
        {
            Assert.IsNotNull(_result);
            Assert.IsInstanceOf<OkObjectResult>(_result);

            var okResult = _result as OkObjectResult;
            var weatherForecasts = okResult?.Value as IEnumerable<WeatherForecast>;

            Assert.IsNotNull(weatherForecasts);
            Assert.IsTrue(weatherForecasts.Any());
        }

        [Test]
        public void SuccessfulRetrievalOfWeatherForecastData()
        {
            GivenTheWeatherForecastEndpointIsAvailable();
            WhenIRequestWeatherForecastData();
            ThenTheResponseShouldBeSuccessfulAndContainWeatherForecastDetails();
        }
    }
}
