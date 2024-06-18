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

namespace GalutinisProjektas.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IATACodesController : ControllerBase
    {
        private readonly IATACodesService _iataCodesService;
        private readonly IMemoryCache _memoryCache;
        private static readonly string IATAcacheKey = "IATACodes";

        public IATACodesController(IATACodesService iATACodesService, IMemoryCache memoryCache)
        {
            _iataCodesService = iATACodesService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        ///  Get all IATA Codes
        /// </summary>
        /// <returns>All Country Codes</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IATACodes>>> GetIATACodes()
        {

           try
            {
                if (!_memoryCache.TryGetValue(IATAcacheKey, out IEnumerable<IATACodes> cacheEntry))
                {
                    var iataCodes = await _iataCodesService.GetIATACodesAsync();
                    if (iataCodes == null)
                    {
                        return NotFound();
                    }
                     cacheEntry = iataCodes.Select(x => IATACodesResponse("GET", x)).ToList();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(5))
                        .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    _memoryCache.Set(IATAcacheKey, cacheEntry, cacheEntryOptions);

                }
                return Ok(cacheEntry);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }
        }

        /// <summary>
        ///  Get IATA Code by ID
        /// </summary>
        /// <param name="id"></param> IATA Codes ID
        /// <returns>Returns IATA Code By ID</returns> 
        [HttpGet("{id}")]
        public async Task<ActionResult<IATACodes>> GetIATACodes([Required]  int id)
        {
            try
            {
                var cacheKey = $"{IATAcacheKey}{id}";
                if (!_memoryCache.TryGetValue(cacheKey, out IATACodesResponse cacheEntry))
                {
                    var iataCode = await _iataCodesService.GetIATACodeByIdAsync(id);
                    if (iataCode == null)
                    {
                        return NotFound();
                    }
                    cacheEntry = IATACodesResponse("GETID", iataCode);

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
        /// Get IATA Code Information by IATA code
        /// </summary>
        /// <param name="IATA"></param> IATA Code
        /// <returns>Returns IATA Code information</returns>
        [HttpGet("GetByIATA/{IATA}")]
        public async Task<ActionResult<IATACodes>> GetByIATA( [Required] string IATA)
        {
            try
            {
                var cacheKey = $"{IATAcacheKey}{IATA}";
                if (!_memoryCache.TryGetValue(cacheKey, out IATACodesResponse cacheEntry))
                {
                    var iataCode = await _iataCodesService.GetIATACodeByCodeAsync(IATA);
                    if (iataCode == null)
                    {
                        return NotFound();
                    }
                    cacheEntry = IATACodesResponse("GETBYIATA", iataCode);

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
        private IATACodesResponse IATACodesResponse(string method, IATACodes iataCodes)
        {
            var iataCodesResponse = new IATACodesResponse
            {
                Id = iataCodes.Id,
                IATA = iataCodes.IATA,
                City = iataCodes.City,
                Country = iataCodes.Country,
                AirportName = iataCodes.AirportName,
                Links = HATEOASLinks(method, iataCodes)
            };

            return iataCodesResponse;
        }
        private List<HATEOASLink> HATEOASLinks(string method, IATACodes iataCodes)
        {
            if (Url == null)
            {
                return new List<HATEOASLink>();
            }

            var links = new List<HATEOASLink>
            {
                (method == "GET") ? new HATEOASLink { Href = Url.Action("GetIATACodes", null), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = "/IATACodes", Rel = "Get all IATA Codes", Method = "GET" },
                (method == "GETID") ? new HATEOASLink { Href = Url.Action("GetIATACodes", new { id = iataCodes.Id }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetIATACodes", new { id = iataCodes.Id }), Rel = "Get IATA Code by ID", Method = "GET" },
                (method == "GETBYIATA") ? new HATEOASLink { Href = Url.Action("GetByIATA", new { iataCodes.IATA }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetByIATA", new { iataCodes.IATA }), Rel = "Get IATA code information by IATA code", Method = "GET" }
            };

            return links;
        }



    }
}
