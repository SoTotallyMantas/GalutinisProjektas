using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using GalutinisProjektas.Server.Controllers;
using GalutinisProjektas.Server.Models.Carbon;
using GalutinisProjektas.Server.Models.ElectricityResponse;
using GalutinisProjektas.Server.Models.FlightResponse;
using GalutinisProjektas.Server.Models.FuelCombustionResponse;
using GalutinisProjektas.Server.Service;
using GalutinisProjektas.Server.Models.UtilityModels;

namespace ReqNrollTests.StepDefinitions
{
    [Binding]
    public class CarbonEmissionsEstimationStepDefinitions
    {
        private Mock<CarbonInterfaceService> _serviceMock;
        private CarbonInterfaceController _controller;
        private ActionResult<ElectricityEstimateResponse> _electricityResult;
        private ActionResult<FlightEstimateResponse> _flightResult;
        private ActionResult<FuelCumbustionEstimateResponse> _fuelResult;

        public CarbonEmissionsEstimationStepDefinitions()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            _serviceMock = new Mock<CarbonInterfaceService>(MockBehavior.Strict, new HttpClient(), Mock.Of<ILogger<CarbonInterfaceService>>(), mockConfiguration.Object);
            _controller = new CarbonInterfaceController(Mock.Of<ILogger<CarbonInterfaceController>>(), _serviceMock.Object);
        }

        [Given("the carbon interface service is available")]
        public void GivenTheCarbonInterfaceServiceIsAvailable()
        {
            // Service mock is already setup in constructor
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

            var result = await _controller.GetElectricityEstimate(request);
            _electricityResult = ConvertToActionResult<ElectricityEstimateResponse>(result);
        }

        [Then("the response should be successful and contain the electricity estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheElectricityEstimate()
        {
            Assert.IsNotNull(_electricityResult);
            Assert.IsInstanceOf<OkObjectResult>(_electricityResult.Result);

            var okResult = _electricityResult.Result as OkObjectResult;
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
                legs = new FlightLegs[]
                {
                    new FlightLegs { departure_airport = "LAX", destination_airport = "JFK" }
                },
                distance_unit = "km"
            };

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

            var result = await _controller.GetFlightEstimate(request);
            _flightResult = ConvertToActionResult<FlightEstimateResponse>(result);
        }

        [Then("the response should be successful and contain the flight estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFlightEstimate()
        {
            Assert.IsNotNull(_flightResult);
            Assert.IsInstanceOf<OkObjectResult>(_flightResult.Result);

            var okResult = _flightResult.Result as OkObjectResult;
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

            var result = await _controller.GetFuelEstimate(request);
            _fuelResult = ConvertToActionResult<FuelCumbustionEstimateResponse>(result);
        }

        [Then("the response should be successful and contain the fuel combustion estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFuelCombustionEstimate()
        {
            Assert.IsNotNull(_fuelResult);
            Assert.IsInstanceOf<OkObjectResult>(_fuelResult.Result);

            var okResult = _fuelResult.Result as OkObjectResult;
            var fuelEstimate = okResult?.Value as FuelCumbustionEstimateResponse;

            Assert.IsNotNull(fuelEstimate);
            Assert.AreEqual("789", fuelEstimate.Data.Id);
        }

        private ActionResult<T> ConvertToActionResult<T>(IActionResult result) where T : class
        {
            if (result is OkObjectResult okResult && okResult.Value is T value)
            {
                return new ActionResult<T>(value);
            }
            else if (result is ObjectResult objectResult && objectResult.Value is T objValue)
            {
                return new ActionResult<T>(objValue);
            }
            return new ActionResult<T>(result as T);
        }
    }
}
