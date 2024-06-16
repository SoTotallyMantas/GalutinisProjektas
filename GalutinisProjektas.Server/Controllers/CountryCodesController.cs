using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Models.HATEOASResourceModels;
using GalutinisProjektas.Server.Service;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryCodesController : ControllerBase
    {
        private readonly ModeldbContext _context;
        private readonly CountryCodesService _countryCodesService;

        public CountryCodesController(ModeldbContext context, CountryCodesService countryCodesService)
        {
            _context = context;
            _countryCodesService = countryCodesService;
        }

        /// <summary>
        /// Retrieves all country codes
        /// </summary>
        /// <returns>All Country Codes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryCodeResponse>>> GetCountryCodes()
        {
            try
            {
               var CountryCodes = await _countryCodesService.GetAllCountryCodesAsync();
                var response = CountryCodes.Select(x => CountryCodeResponse("GET", x)).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves Country Codes by ID
        /// </summary>
        /// <param name="id"></param> Country Codes ID
        /// <returns>Returns Country Code By ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryCodeResponse>> GetCountryCodes( [Required] int id)
        {
            try
            {
                var countryCodes = await _countryCodesService.GetCountryCodeByIdAsync(id);

                if (countryCodes == null)
                {
                    return NotFound();
                }
                var response = CountryCodeResponse("GETID", countryCodes);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        
        /// <summary>
        ///  Retrieves Country Codes by Country Name
        /// </summary> 
        /// <param name="CountryName"></param> Country Name
        /// <remarks>Queries the database for the country code based on the specified country name</remarks>
        /// <returns>Country Code</returns>

        [HttpGet("GetByName/{CountryName}")]
        public async Task<ActionResult<CountryCodeResponse>> GetByCountryName([Required] string CountryName)
        {
            try
            {
                var countryCodes = await _countryCodesService.GetCountryCodeByCountryNameAsync(CountryName);

                if (countryCodes == null)
                {
                    return NotFound();
                }
                var response = CountryCodeResponse("GETBYNAME", countryCodes);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        private CountryCodeResponse CountryCodeResponse(string method, CountryCodes countryCodes)
        {
            var countryCodeResponse = new CountryCodeResponse
            {
                Id = countryCodes.Id,
                CountryCode = countryCodes.CountryCode,
                CountryName = countryCodes.CountryName,
                Links = HATEOASLinks(method, countryCodes)
            };
            
            return countryCodeResponse;
        }
        private List<HATEOASLink> HATEOASLinks(string method,CountryCodes countryCodes) {
            var links = new List<HATEOASLink>
            {
               (method == "GET") ? new HATEOASLink { Href = Url.Action("GetCountryCodes", null), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = "/CountryCodes", Rel = "Get all country codes", Method = "GET" },
               (method == "GETID") ? new HATEOASLink { Href = Url.Action("GetCountryCodes", new { id = countryCodes.Id }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetCountryCodes", new { id = countryCodes.Id }), Rel = "Get country code by ID", Method = "GET" },
               (method == "GETBYNAME") ? new HATEOASLink { Href = Url.Action("GetByCountryName", new { CountryName = countryCodes.CountryName }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetByCountryName", new { CountryName = countryCodes.CountryName }), Rel = "Get country code by country name", Method = "GET" }
            };
            return links;
        }
    }
}
