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
using GalutinisProjektas.Server.Service;

namespace ReqNrollTests.StepDefinitions
{
    [Binding]
    public class CarbonEmissionsEstimationStepDefinitions
    {
        private Mock<ICarbonInterfaceService> _serviceMock;
        private CarbonInterfaceController _controller;
        private ActionResult<ElectricityEstimateResponse> _electricityResult;
        private ActionResult<FlightEstimateResponse> _flightResult;
        private ActionResult<FuelCumbustionEstimateResponse> _fuelResult;

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

            var result = await _controller.GetFlightEstimate(request);
            _flightResult = ConvertToActionResult<FlightEstimateResponse>(result);
        }

        [Then("the response should be successful and contain the flight estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFlightEstimate()
        {
            Assert.IsNotNull(_flightResult);
            Assert.IsInstanceOf<ObjectResult>(_flightResult.Result);

            var okResult = _flightResult.Result as ObjectResult;
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

            var result = await _controller.GetFuelEstimate(request);
            _fuelResult = ConvertToActionResult<FuelCumbustionEstimateResponse>(result);
        }

        [Then("the response should be successful and contain the fuel combustion estimate")]
        public void ThenTheResponseShouldBeSuccessfulAndContainTheFuelCombustionEstimate()
        {
            Assert.IsNotNull(_fuelResult);
            Assert.IsInstanceOf<ObjectResult>(_fuelResult.Result);

            var okResult = _fuelResult.Result as ObjectResult;
            var fuelEstimate = okResult?.Value as FuelCumbustionEstimateResponse;

            Assert.IsNotNull(fuelEstimate);
            Assert.AreEqual("789", fuelEstimate.Data.Id);
        }

        private ActionResult<T> ConvertToActionResult<T>(IActionResult result) where T : class
        {
            if (result is ObjectResult okResult && okResult.Value is T value)
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
