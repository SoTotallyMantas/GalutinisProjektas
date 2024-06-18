using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Models.Carbon;
using GalutinisProjektas.Server.Models.ElectricityResponse;
using GalutinisProjektas.Server.Models.FlightResponse;
using GalutinisProjektas.Server.Models.FuelCombustionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GalutinisProjektas.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarbonInterfaceController : Controller
    {
        private readonly ILogger<CarbonInterfaceController> _logger;
        private readonly ICarbonInterfaceService _carbonInterfaceService;

        public CarbonInterfaceController(ILogger<CarbonInterfaceController> logger, ICarbonInterfaceService carbonInterfaceService)
        {
            _carbonInterfaceService = carbonInterfaceService;
            _logger = logger;
        }
        /// <summary>
        ///    Retrieves the carbon emissions estimate for electricity usage
        /// </summary>
        /// 
        /// <param name="request"></param> CarbonElectricity object containing the electricity unit, value, country 
        /// <returns>Electricity carbon emissions Estimate</returns>
        /// <remarks>
        ///  Queries the CarbonInterface API for carbon emissions estimate based on Country, Electricity unit and value
        ///  </remarks>
        ///  <response code="201">Returns the carbon emissions estimate for electricity usage</response>
        ///  <response code="400">If the request is invalid</response>
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
                    _logger.LogError($"Error: {result.ErrorMessage}, StatusCode: {result.StatusCode}");
                    return StatusCode(result.StatusCode, result.ErrorMessage);
                }

                var electricityEstimateResponse = result.Data;

                if (Url == null)
                {
                    _logger.LogWarning("Url is null, cannot add HATEOAS link.");
                    electricityEstimateResponse.Links.Add(new HATEOASLink
                    {
                        Href = null,
                        Rel = "self",
                        Method = "Post"
                    });
                }
                else
                {
                    electricityEstimateResponse.Links.Add(new HATEOASLink
                    {
                        Href = Url.Action("Electricity", new { request.electricity_unit, request.electricity_value, request.country }),
                        Rel = "self",
                        Method = "Post"
                    });
                }

                return Ok(electricityEstimateResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while fetching data from Carbon Interface API: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /// <summary>
        /// Retrieves the carbon emissions estimate for a flight
        /// </summary>
        /// <param name="request"></param> CarbonFlight object containing the flight distance, distance unit, country and flight destination/departure
        /// <returns> Flight carbon emissions estimate</returns>
        /// <remarks> 
        /// Queries the CarbonInterface API for carbon emissions estimate based on flight distance, distance unit, country and flight destination/departure
        ///  </remarks>
        /// <response code="201">Returns the carbon emissions estimate for Flight</response>
        ///  <response code="400">If the request is invalid</response>
        [HttpPost("Flight", Name = "Flight")]
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
                    _logger.LogError($"Error: {serviceResponse.ErrorMessage}, StatusCode: {serviceResponse.StatusCode}");
                    return StatusCode(serviceResponse.StatusCode, serviceResponse.ErrorMessage);
                }

                var flightEstimateResponse = serviceResponse.Data;

                if (flightEstimateResponse == null)
                {
                    _logger.LogError("flightEstimateResponse is null.");
                    return StatusCode(500, "Internal server error: flightEstimateResponse is null.");
                }

                if (flightEstimateResponse.Links == null)
                {
                    flightEstimateResponse.Links = new List<HATEOASLink>();
                }

                flightEstimateResponse.Links.Add(new HATEOASLink
                {
                    Href = Url?.Link("Flight", null),
                    Rel = "self",
                    Method = "Post"
                });

                return Ok(flightEstimateResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while fetching data from Carbon Interface API: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves the carbon emissions estimate for fuel combustions
        /// </summary>
        /// <param name="request"></param> CarbonFuelCombustion object containing the Fuel source type, unit, value
        /// <returns> Fuel combustion carbon emissions estimate</returns>
        /// <remarks> 
        /// Queries the CarbonInterface API for carbon emissions estimate based on Fuel source type, unit, value
        ///  </remarks>
        /// <response code="201">Returns the carbon emissions estimate for Fuel Combustion</response>
        ///  <response code="400">If the request is invalid</response>
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
                    _logger.LogError($"Error: {serviceResponse.ErrorMessage}, StatusCode: {serviceResponse.StatusCode}");
                    return StatusCode(serviceResponse.StatusCode, serviceResponse.ErrorMessage);
                }

                var fuelCumbustionEstimateResponse = serviceResponse.Data;

                if (fuelCumbustionEstimateResponse == null)
                {
                    _logger.LogError("fuelCumbustionEstimateResponse is null.");
                    return StatusCode(500, "Internal server error: fuelCumbustionEstimateResponse is null.");
                }

                if (fuelCumbustionEstimateResponse.Links == null)
                {
                    fuelCumbustionEstimateResponse.Links = new List<HATEOASLink>();
                }

                fuelCumbustionEstimateResponse.Links.Add(new HATEOASLink
                {
                    Href = Url?.Link("Fuel", new { request.fuel_source_type, request.fuel_source_unit, request.fuel_source_value }),
                    Rel = "self",
                    Method = "Post"
                });

                return Ok(fuelCumbustionEstimateResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while fetching data from Carbon Interface API: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
