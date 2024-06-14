using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Models.Carbon;
using GalutinisProjektas.Server.Models.ElectricityResponse;
using GalutinisProjektas.Server.Models.FlightResponse;
using GalutinisProjektas.Server.Models.FuelCombustionResponse;
using GalutinisProjektas.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalutinisProjektas.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarbonInterfaceController : Controller
    {
        private readonly ILogger<CarbonInterfaceController> _logger;
        private readonly CarbonInterfaceService _carbonInterfaceService;

        public CarbonInterfaceController(ILogger<CarbonInterfaceController> logger, CarbonInterfaceService carbonInterfaceService)
        {
            _carbonInterfaceService = carbonInterfaceService;
            _logger = logger;
        }

        [HttpPost("Electricity", Name = "Electricity")]
        [ProducesResponseType(typeof(ElectricityEstimateResponse), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetElectricityEstimate([FromQuery] CarbonElectricity request)
        {
            try
            {
                request.type = "electricity";
                var result = await _carbonInterfaceService.GetElectricityEstimateAsync(request);

                if (result.StatusCode != 201)
                {
                    return StatusCode(result.StatusCode, result.ErrorMessage);
                }
                var electricityEstimateResponse = result.Data;
                var state = request.state == null ? String.Empty : request.state;
                electricityEstimateResponse.Links.Add(new HATEOASLink
                {
                    Href = Url.Link("Electricity", new { request.electricity_unit, request.electricity_value, request.country, state }),
                    Rel = "self",
                    Method = "Post"
                });

                return Ok(electricityEstimateResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while fetching data from Carbon Interface API: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpPost("Flight", Name ="Flight")]
        [ProducesResponseType(typeof(FlightEstimateResponse), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFlightEstimate([FromBody] CarbonFlight request)
        {
            try
            {
                request.type = "flight";
                var serviceResponse = await _carbonInterfaceService.GetFlightEstimateAsync(request);

                if (serviceResponse.StatusCode != 201)
                {
                    return StatusCode(serviceResponse.StatusCode, serviceResponse.ErrorMessage);
                }
                var flightEstimateResponse = serviceResponse.Data;
                flightEstimateResponse.Links.Add(new HATEOASLink
                {
                    Href =  Url.Link("Flight",null),
                    Rel = "self",
                    Method = "Post"
                });
                return Ok(flightEstimateResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while fetching data from Carbon Interface API: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Fuel", Name = "Fuel")]
        [ProducesResponseType(typeof(FuelCumbustionEstimateResponse), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFuelEstimate([FromQuery] CarbonFuelCombustion request)
        {
            try
            {
                request.type = "fuel_combustion";
                var serviceResponse = await _carbonInterfaceService.GetFuelEstimateAsync(request);
                if (serviceResponse.StatusCode != 201)
                {
                    return StatusCode(serviceResponse.StatusCode, serviceResponse.ErrorMessage);
                }
                var fuelCumbustionEstimateResponse = serviceResponse.Data;
                fuelCumbustionEstimateResponse.Links.Add(new HATEOASLink
                {
                    Href = Url.Link("Fuel", new { request.fuel_source_type, request.fuel_source_unit, request.fuel_source_value }),
                    Rel = "self",
                    Method = "Post"
                });
                return Ok(fuelCumbustionEstimateResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
