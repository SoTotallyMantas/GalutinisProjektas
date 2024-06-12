using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Models.AirPollutionResponse;
using GalutinisProjektas.Server.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GalutinisProjektas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenWeatherMapController : Controller
    {
        private readonly ILogger<OpenWeatherMapController> _logger;
        private readonly OpenWeatherMapService _openWeatherMapService;
        
        

        public OpenWeatherMapController(ILogger<OpenWeatherMapController> logger,OpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
            _logger = logger;
        }
        
        [HttpGet(Name = "air-pollution")]
        public async Task<ActionResult<AirPollutionResponse>> Get(double latitude, double longitude)
        {
            var serviceResponse = await _openWeatherMapService.GetAirPollutionDataAsync(latitude, longitude);
            if(serviceResponse.StatusCode != 200)
            {
                return StatusCode(serviceResponse.StatusCode, serviceResponse.ErrorMessage);
            }

            var airPollutionResponse = serviceResponse.Data;
            airPollutionResponse.Links.Add(new HATEOASLink
            {
                Href = Url.Link("air-pollution", new { latitude, longitude }),
                Rel = "self",
                Method = "GET"
            });
            return Ok(airPollutionResponse);
            
           
        }

       
    }
}
