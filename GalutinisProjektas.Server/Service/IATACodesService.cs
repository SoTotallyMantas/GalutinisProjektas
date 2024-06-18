using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.EntityFrameworkCore;

namespace GalutinisProjektas.Server.Service
{
    /// <summary>
    /// Service class for handling operations related to IATA codes.
    /// </summary>
    public class IATACodesService
    {
        private readonly ModeldbContext _context;

        /// <summary>
        /// Constructor for IATACodesService class.
        /// </summary>
        /// <param name="context">Database context instance.</param>
        public IATACodesService(ModeldbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all IATA codes asynchronously.
        /// </summary>
        /// <returns>A collection of all IATA codes.</returns>
        public async Task<IEnumerable<IATACodes>> GetIATACodesAsync()
        {
            return await _context.IATACodes.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves an IATA code by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the IATA code.</param>
        /// <returns>The IATA code entity.</returns>
        public async Task<IATACodes> GetIATACodeByIdAsync(int id)
        {
            return await _context.IATACodes.FindAsync(id);
        }

        /// <summary>
        /// Retrieves an IATA code by its code asynchronously.
        /// </summary>
        /// <param name="IATA">The IATA code.</param>
        /// <returns>The IATA code entity.</returns>
        public async Task<IATACodes> GetIATACodeByCodeAsync(string IATA)
        {
            return await _context.IATACodes.FirstOrDefaultAsync(x => x.IATA == IATA);
        }

        internal async Task<IEnumerable<IATACodes>> GetIATACodesByCountryAsync(string country)
        {
           return await _context.IATACodes.Where(x => x.Country == country).ToListAsync();
        }
    }
}

