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
using Microsoft.Extensions.Caching.Memory;
using Azure;

namespace GalutinisProjektas.Server.Controllers
{
    /// <summary>
    /// Controller responsible for handling requests related to country codes.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CountryCodesController : ControllerBase
    {
        
        private readonly CountryCodesService _countryCodesService;
        private readonly IMemoryCache _memoryCache;
        private static readonly string CountryCodesCacheKey = "CountryCodes";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryCodesController"/> class.
        /// </summary>
        /// <param name="countryCodesService">Service instance for handling country codes operations.</param>
        /// <param name="memoryCache">Memory cache instance for caching country codes.</param>
        public CountryCodesController(CountryCodesService countryCodesService,IMemoryCache memoryCache)
        {
            _countryCodesService = countryCodesService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Retrieves all country codes.
        /// </summary>
        /// <response code="200">Returns the list of all country codes.</response>
        /// <response code="404">If no country codes are found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        /// <returns>All Country Codes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryCodeResponse>>> GetCountryCodes()
        {
            try
            {
                if (!_memoryCache.TryGetValue(CountryCodesCacheKey, out IEnumerable<CountryCodes> cacheEntry))
                {
                    var countryCodes = await _countryCodesService.GetAllCountryCodesAsync();
                    if (countryCodes.Count() == 0)
                    {
                        return NotFound();
                    }
                    cacheEntry = countryCodes.Select(x => CountryCodeResponse("GET", x)).ToList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(5))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(CountryCodesCacheKey, cacheEntry, cacheEntryOptions);
                }
               
                return Ok(cacheEntry);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

         /// <summary>
        /// Retrieves country code by ID.
        /// </summary>
        /// <param name="id">Country code ID.</param>
        /// <returns>Returns country code by ID.</returns>
        /// <response code="200">Returns the country code for the specified ID.</response>
        /// <response code="404">If no country code is found for the specified ID.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryCodeResponse>> GetCountryCodes( [Required] int id)
        {
            try
            {
                string cacheKey = $"{CountryCodesCacheKey}{id}";
                if (!_memoryCache.TryGetValue(cacheKey, out CountryCodes cacheEntry))
                {
                    var countryCodes = await _countryCodesService.GetCountryCodeByIdAsync(id);

                    if (countryCodes == null)
                    {
                        return NotFound();
                    }
                    cacheEntry = countryCodes;
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(5))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
                }
               

                return Ok(cacheEntry);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        
        /// <summary>
        /// Retrieves country code by country name.
        /// </summary> 
        /// <param name="CountryName">Country name.</param>
        /// <remarks>Queries the database for the country code based on the specified country name.</remarks>
        /// <returns>Country code for the specified country name.</returns>
        /// <response code="200">Returns the country code for the specified country name.</response>
        /// <response code="404">If no country code is found for the specified country name.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("GetByName/{CountryName}")]
        public async Task<ActionResult<CountryCodeResponse>> GetByCountryName([Required] string CountryName)
        {
            try
            {
                string cacheKey = $"{CountryCodesCacheKey}{CountryName}";
                if(!_memoryCache.TryGetValue(cacheKey, out CountryCodes cacheEntry))
                {

                    var countryCodes = await _countryCodesService.GetCountryCodeByCountryNameAsync(CountryName);

                    if (countryCodes == null)
                    {
                        return NotFound();
                    }
                    cacheEntry = CountryCodeResponse("GETBYNAME", countryCodes);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(5))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                     _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);

                }
                return Ok(cacheEntry);



            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        
        /// <summary>
        /// Creates a CountryCodeResponse object with HATEOAS links.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="countryCodes">Country codes entity.</param>
        /// <returns>Returns a CountryCodeResponse object.</returns>
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
        
        /// <summary>
        /// Generates HATEOAS links for a CountryCodes entity.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="countryCodes">Country codes entity.</param>
        /// <returns>Returns a list of HATEOAS links.</returns>
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
