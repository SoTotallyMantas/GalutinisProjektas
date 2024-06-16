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

namespace GalutinisProjektas.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        private readonly FuelTypesService _fuelTypesService;

        public FuelTypesController(FuelTypesService fuelTypesService)
        {
            _fuelTypesService = fuelTypesService;
        }

        /// <summary>
        /// Get All Fuel Types
        /// </summary>
        /// <returns>Returns All Fuel Types</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelTypes>>> GetFuelTypes()
        {
            try
            {
                var fuelTypes = await _fuelTypesService.GetFuelTypesAsync();
                if (fuelTypes == null)
                {
                    return NotFound();
                }

                var response = fuelTypes.Select(x => FuelTypesResponse("GET", x)).ToList();
                return Ok(response);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }

        }
        /// <summary>
        /// Get Fuel Type by ID
        /// </summary>
        /// <param name="id"></param> Fuel Type ID
        /// <returns>Fuel Type by ID</returns>
       
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelTypes>> GetFuelTypes( [Required] int id)
        {
            try
            {
                var fuelTypes = await _fuelTypesService.GetFuelTypeByIdAsync(id);

                if (fuelTypes == null)
                {
                    return NotFound();
                }
                var response = FuelTypesResponse("GETID", fuelTypes);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }
        }
       
        /// <summary>
        ///  Get Fuel Type Information by Fuel Type
        /// </summary>
        /// <param name="FuelType"></param> Fuel Type
        /// <returns>Returns Fuel Type information</returns>
        [HttpGet("GetByFuelType/{FuelType}")]
        public async Task<ActionResult<FuelTypes>> GetByFuelType( [Required] string FuelType)
        {
            try
            {
                var fuelTypes = await _fuelTypesService.GetFuelTypeByNameAsync(FuelType);

                if (fuelTypes == null)
                {
                    return NotFound();
                }
                var response = fuelTypes.Select(x => FuelTypesResponse("GETBYTYPE", x)).ToList();
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }
        }
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
        
