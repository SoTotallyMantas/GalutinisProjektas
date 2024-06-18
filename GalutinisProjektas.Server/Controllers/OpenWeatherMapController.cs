using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GalutinisProjektas.Server.Models.AirPollutionResponse;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GalutinisProjektas.Server.Models.UtilityModels;
using System;
using GalutinisProjektas.Server.Interface;
using System.Collections.Generic;
using GalutinisProjektas.Server.Interfaces;

namespace GalutinisProjektas.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenWeatherMapController : Controller
    {
        private const string RouteName = "air-pollution";
        private readonly ILogger<OpenWeatherMapController> _logger;
        private readonly IOpenWeatherMapService _openWeatherMapService;

        public OpenWeatherMapController(ILogger<OpenWeatherMapController> logger, IOpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
            _logger = logger;
        }

        [HttpPost(Name = RouteName)]
        [ProducesResponseType(typeof(AirPollutionResponse), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AirPollutionResponse>> Post([Required] double latitude, [Required] double longitude)
        {
            try
            {
                var serviceResponse = await _openWeatherMapService.GetAirPollutionDataAsync(latitude, longitude);
                if (serviceResponse == null)
                {
                    return StatusCode(500, "Internal server error");
                }

                if (serviceResponse.StatusCode != 200)
                {
                    return StatusCode(serviceResponse.StatusCode, serviceResponse.ErrorMessage);
                }

                var airPollutionResponse = serviceResponse.Data;
                if (airPollutionResponse == null)
                {
                    return StatusCode(500, "Internal server error");
                }

                airPollutionResponse.Links = new List<HATEOASLink>
                {
                    new HATEOASLink
                    {
                        Href = Url.Action(nameof(Post), new { latitude, longitude }),
                        Rel = "self",
                        Method = "POST"
                    }
                };

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
