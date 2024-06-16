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
    public class IATACodesController : ControllerBase
    {
        private readonly IATACodesService _iataCodesService;

        public IATACodesController(IATACodesService iATACodesService)
        {
            _iataCodesService = iATACodesService;
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
                var iataCodes = await _iataCodesService.GetIATACodesAsync();
                if (iataCodes == null)
                {
                    return NotFound();
                }
                var response = iataCodes.Select(x => IATACodesResponse("GET", x)).ToList();
                return Ok(response);
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
                var iATACodes = await _iataCodesService.GetIATACodeByIdAsync(id);

                if (iATACodes == null)
                {
                    return NotFound();
                }
                var response = IATACodesResponse("GETID", iATACodes);
                return Ok(response);
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
                var iATACodes = await _iataCodesService.GetIATACodeByCodeAsync(IATA);

                if (iATACodes == null)
                {
                    return NotFound();
                }
                var response = IATACodesResponse("GETBYIATA", iATACodes);

                return Ok(response);
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
            var links = new List<HATEOASLink>
            {
               (method == "GET") ? new HATEOASLink { Href = Url.Action("GetIATACodes", null), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = "/IATACodes", Rel = "Get all IATA Codes", Method = "GET" },
               (method == "GETID") ? new HATEOASLink { Href = Url.Action("GetIATACodes", new { id = iataCodes.Id }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetIATACodes", new { id = iataCodes.Id }), Rel = "Get IATA Code by ID", Method = "GET" },
               (method == "GETBYIATA") ? new HATEOASLink { Href = Url.Action("GetByIATA", new {  iataCodes.IATA }), Rel = "Self", Method = "GET" } : new HATEOASLink { Href = Url.Action("GetByIATA", new { iataCodes.IATA }), Rel = "Get IATA code information by IATA code", Method = "GET" }
            };
            return links;
        }



    }
}
