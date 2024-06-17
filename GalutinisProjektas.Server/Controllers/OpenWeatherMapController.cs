using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Models.AirPollutionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GalutinisProjektas.Server.Controllers
{
    ///<summary>
    /// Controller for retrieving air pollution data
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    
    public class OpenWeatherMapController : Controller
    {
        private const string RouteName = "air-pollution";
        private readonly ILogger<OpenWeatherMapController> _logger;
        private readonly OpenWeatherMapService _openWeatherMapService;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenWeatherMapController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging.</param>
        /// <param name="openWeatherMapService">Service instance for interacting with the OpenWeatherMap API.</param>
        public OpenWeatherMapController(ILogger<OpenWeatherMapController> logger,OpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves air pollution data based on coordinates.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <returns>The air pollution data for the specified location.</returns>
        /// <remarks>
        /// Queries the OpenWeatherMap API for air pollution data based on the specified coordinates.
        /// </remarks>
        /// <response code="200">Returns the air pollution data for the specified location.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost(Name = RouteName)]
        [ProducesResponseType(typeof(AirPollutionResponse), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AirPollutionResponse>> Get([Required] double latitude, [Required] double longitude)
        {
            try
            {
                var serviceResponse = await _openWeatherMapService.GetAirPollutionDataAsync(latitude, longitude);
                if (serviceResponse.StatusCode != 201)
                {
                    return StatusCode(serviceResponse.StatusCode, serviceResponse.ErrorMessage);
                }

                var airPollutionResponse = serviceResponse.Data;
                airPollutionResponse.Links.Add(new HATEOASLink
                {
                    Href = Url.Link(RouteName, new { latitude, longitude }),
                    Rel = "self",
                    Method = "Post"
                });
                return Ok(airPollutionResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while fetching data from OpenWeatherMap API: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            
           
        }

       
    }
}
