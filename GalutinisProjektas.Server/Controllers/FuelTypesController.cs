using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.UtilityModels;
using GalutinisProjektas.Server.Service;
using GalutinisProjektas.Server.Models.HATEOASResourceModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Caching.Memory;

namespace GalutinisProjektas.Server.Controllers
{
    /// <summary>
    /// Controller responsible for handling requests related to fuel types.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        private readonly FuelTypesService _fuelTypesService;
        private readonly IMemoryCache _memoryCache;
        private static readonly string FuelTypesCacheKey = "FuelTypes";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FuelTypesController"/> class.
        /// </summary>
        /// <param name="fuelTypesService">Service instance for handling fuel types operations.</param>
        /// <param name="memoryCache">Memory cache instance for caching fuel types.</param>
        public FuelTypesController(FuelTypesService fuelTypesService, IMemoryCache memoryCache)
        {
            _fuelTypesService = fuelTypesService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get All Fuel Types
        /// </summary>
        /// <returns>Returns All Fuel Types</returns>
        /// <response code="200">Returns the list of all fuel types.</response>
        /// <response code="404">If no fuel types are found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelTypes>>> GetFuelTypes()
        {
            try
            {
                if(!_memoryCache.TryGetValue(FuelTypesCacheKey, out IEnumerable<FuelTypes> cacheEntry))
                {
                    var fuelTypes = await _fuelTypesService.GetFuelTypesAsync();
                    if (fuelTypes.Count() == 0)
                    {
                        return NotFound();
                    }
                    cacheEntry = fuelTypes.Select(x => FuelTypesResponse("GET", x)).ToList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(5))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(FuelTypesCacheKey, cacheEntry, cacheEntryOptions);
                }
               

               
                return Ok(cacheEntry);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }

        }
        
        /// <summary>
        /// Get Fuel Type by ID
        /// </summary>
        /// <param name="id">Fuel type ID.</param>
        /// <returns>Fuel type by ID.</returns>
        /// <response code="200">Returns the fuel type for the specified ID.</response>
        /// <response code="404">If no fuel type is found for the specified ID.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelTypes>> GetFuelTypes( [Required] int id)
        {
            try
            {
                string cacheKey = $"{FuelTypesCacheKey}{id}";
                if (!_memoryCache.TryGetValue(cacheKey, out FuelTypesResponse cacheEntry))
                {
                    var fuelTypes = await _fuelTypesService.GetFuelTypeByIdAsync(id);

                    if (fuelTypes == null)
                    {
                        return NotFound();
                    }
                    cacheEntry = FuelTypesResponse("GETID", fuelTypes);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(5))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
                }
               
                return Ok(cacheEntry);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }
        }
       
        /// <summary>
        ///  Get Fuel Type Information by Fuel Type
        /// </summary>
        /// <param name="FuelType">Fuel type name.</param>
        /// <returns>Fuel type information.</returns>
        /// <response code="200">Returns the fuel type information for the specified fuel type name.</response>
        /// <response code="404">If no fuel type is found for the specified fuel type name.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("GetByFuelType/{FuelType}")]
        public async Task<ActionResult<FuelTypes>> GetByFuelType( [Required] string FuelType)
        {
            try
            {
                string cacheKey = $"{FuelTypesCacheKey}{FuelType}";
                if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<FuelTypes> cacheEntry))
                    {
                    var fuelTypes = await _fuelTypesService.GetFuelTypeByNameAsync(FuelType);

                    if (fuelTypes.Count() == 0)
                    {
                        return NotFound();
                    }
                    cacheEntry = fuelTypes.Select(x => FuelTypesResponse("GETBYTYPE", x)).ToList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(5))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
                }
                return Ok(cacheEntry);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }
        }

        /// <summary>
        /// Creates a FuelTypesResponse object with HATEOAS links.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="fuelTypes">Fuel types entity.</param>
        /// <returns>Returns a FuelTypesResponse object.</returns>
        private FuelTypesResponse FuelTypesResponse(string method, FuelTypes fuelTypes)
        {
            var fuelTypesResponse = new FuelTypesResponse
            {
                Id = fuelTypes.Id,
                FuelType = fuelTypes.FuelType,
                FuelName = fuelTypes.FuelName,
                FuelUnit = fuelTypes.FuelUnit,
                Links = HATEOASLinks(method, fuelTypes)
            };

            return fuelTypesResponse;
        }

        /// <summary>
        /// Generates HATEOAS links for a FuelTypes entity.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="fuelTypes">Fuel types entity.</param>
        /// <returns>Returns a list of HATEOAS links.</returns>
        private List<HATEOASLink> HATEOASLinks(string method, FuelTypes fuelTypes)
        {
            var links = new List<HATEOASLink>
            {
               (method == "GET") ? new HATEOASLink { Href = Url.Action("GetFuelTypes", null), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = "/FuelTypes", Rel = "Get all Fuel Types", Method = "GET" },
               (method == "GETID") ? new HATEOASLink { Href = Url.Action("GetFuelTypes", new { id = fuelTypes.Id }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetFuelTypes", new { id = fuelTypes.Id }), Rel = "Get Fuel Type by ID", Method = "GET" },
               (method == "GETBYTYPE") ? new HATEOASLink { Href = Url.Action("GetByFuelType", new { FuelType = fuelTypes.FuelType }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetByFuelType", new { FuelType = fuelTypes.FuelType }), Rel = "Get Fuel Type by Fuel Types", Method = "GET" }
            };
            return links;
        }


    }
}
        
