using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using GalutinisProjektas.Server.Controllers;
using GalutinisProjektas.Server.Models.Carbon;
using GalutinisProjektas.Server.Models.ElectricityResponse;
using GalutinisProjektas.Server.Models.FlightResponse;
using GalutinisProjektas.Server.Models.FuelCombustionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Interfaces;
using GalutinisProjektas.Server.Interface;

namespace ReqNrollTests.StepDefinitions
{
    [Binding]
    public class CarbonEmissionsEstimationStepDefinitions
    {
        private Mock<ICarbonInterfaceService> _serviceMock;
        private CarbonInterfaceController _controller;
        private IActionResult _electricityResult;
        private IActionResult _flightResult;
        private IActionResult _fuelResult;

        public CarbonEmissionsEstimationStepDefinitions()
        {
            _serviceMock = new Mock<ICarbonInterfaceService>();
            _controller = new CarbonInterfaceController(Mock.Of<ILogger<CarbonInterfaceController>>(), _serviceMock.Object);
        }

        [Given("the carbon interface service is available")]
        public void GivenTheCarbonInterfaceServiceIsAvailable()
        {
            _serviceMock.Setup(s => s.GetElectricityEstimateAsync(It.IsAny<CarbonElectricity>()))
                .ReturnsAsync(new ServiceResponse<ElectricityEstimateResponse>
                {
                    Data = new ElectricityEstimateResponse
                    {
                        Data = new ElectricityResponseData { Id = "123", Type = "electricity", Attributes = new ElectricityResponseAttributes { CarbonKg = 50 } },
                        Links = new System.Collections.Generic.List<HATEOASLink>()
                    },
                    StatusCode = 201
                });

            _serviceMock.Setup(s => s.GetFlightEstimateAsync(It.IsAny<CarbonFlight>()))
                .ReturnsAsync(new ServiceResponse<FlightEstimateResponse>
                {
                    Data = new FlightEstimateResponse
                    {
                        Data = new FlightResponseData { Id = "456", Type = "flight", Attributes = new FlightResponseAttributes { CarbonKg = 200 } },
                        Links = new System.Collections.Generic.List<HATEOASLink>()
                    },
                    StatusCode = 201
                });

            _serviceMock.Setup(s => s.GetFuelEstimateAsync(It.IsAny<CarbonFuelCombustion>()))
                .ReturnsAsync(new ServiceResponse<FuelCumbustionEstimateResponse>
                {
                    Data = new FuelCumbustionEstimateResponse
                    {
                        Data = new FuelCombustionResponseData { Id = "789", Type = "fuel_combustion", Attributes = new FuelCombustionResponseAttributes { CarbonKg = 300 } },
                        Links = new System.Collections.Generic.List<HATEOASLink>()
                    },
                    StatusCode = 201
                });
        }

        [When("I request electricity carbon emission estimate")]
        public async Task WhenIRequestElectricityCarbonEmissionEstimate()
        {
            var request = new CarbonElectricity
            {
                type = "electricity",
                electricity_unit = "kwh",
                electricity_value = 100,
                country = "USA"
            };

            _electricityResult = await _controller.GetElectricityEstimate(request);
            LogResult(_electricityResult);
        }

        [Then("the response should be successful and contain the electricity estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheElectricityEstimate()
        {
            Assert.IsNotNull(_electricityResult);
            Assert.IsInstanceOf<OkObjectResult>(_electricityResult);

            var okResult = _electricityResult as OkObjectResult;
            var electricityEstimate = okResult?.Value as ElectricityEstimateResponse;

            Assert.IsNotNull(electricityEstimate);
            Assert.AreEqual("123", electricityEstimate.Data.Id);
        }

        [When("I request flight carbon emission estimate")]
        public async Task WhenIRequestFlightCarbonEmissionEstimate()
        {
            var request = new CarbonFlight
            {
                type = "flight",
                passengers = 1,
                legs = new List<FlightLegs>()
                {
                    new FlightLegs { departure_airport = "LAX", destination_airport = "JFK" }
                },
                distance_unit = "km"
            };

            _flightResult = await _controller.GetFlightEstimate(request);
            LogResult(_flightResult);
        }

        [Then("the response should be successful and contain the flight estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFlightEstimate()
        {
            Assert.IsNotNull(_flightResult);
            Assert.IsInstanceOf<OkObjectResult>(_flightResult);

            var okResult = _flightResult as OkObjectResult;
            var flightEstimate = okResult?.Value as FlightEstimateResponse;

            Assert.IsNotNull(flightEstimate);
            Assert.AreEqual("456", flightEstimate.Data.Id);
        }

        [When("I request fuel combustion carbon emission estimate")]
        public async Task WhenIRequestFuelCombustionCarbonEmissionEstimate()
        {
            var request = new CarbonFuelCombustion
            {
                type = "fuel_combustion",
                fuel_source_type = "diesel",
                fuel_source_unit = "liter",
                fuel_source_value = 100
            };

            _fuelResult = await _controller.GetFuelEstimate(request);
            LogResult(_fuelResult);
        }

        [Then("the response should be successful and contain the fuel combustion estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFuelCombustionEstimate()
        {
            Assert.IsNotNull(_fuelResult);
            Assert.IsInstanceOf<OkObjectResult>(_fuelResult);

            var okResult = _fuelResult as OkObjectResult;
            var fuelEstimate = okResult?.Value as FuelCumbustionEstimateResponse;

            Assert.IsNotNull(fuelEstimate);
            Assert.AreEqual("789", fuelEstimate.Data.Id);
        }

        private void LogResult(IActionResult result)
        {
            if (result is ObjectResult objectResult)
            {
                Console.WriteLine($"Result: {objectResult.StatusCode}, Value: {objectResult.Value}");
            }
            else
            {
                Console.WriteLine("Result is not an ObjectResult.");
            }
        }
    }
}
