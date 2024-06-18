using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.HATEOASResourceModels;
using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.EntityFrameworkCore;


namespace GalutinisProjektas.Server.Service
{
    /// <summary>
    /// Service class for handling operations related to country codes.
    /// </summary>
    public class CountryCodesService
    {
        private readonly ModeldbContext _context;

        /// <summary>
        /// Constructor for CountryCodesService class.
        /// </summary>
        /// <param name="context">Database context instance.</param>
        public CountryCodesService(ModeldbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all country codes asynchronously.
        /// </summary>
        /// <returns>A collection of all country codes.</returns>
        public async Task<IEnumerable<CountryCodes>> GetAllCountryCodesAsync()
        {
            return await _context.CountryCodes.ToListAsync();
        }

        /// <summary>
        /// Retrieves a country code by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the country code.</param>
        /// <returns>The country code entity.</returns>
        public async Task<CountryCodes> GetCountryCodeByIdAsync(int id)
        {
            return await _context.CountryCodes.FindAsync(id);
        }

        /// <summary>
        /// Retrieves a country code by its country name asynchronously.
        /// </summary>
        /// <param name="countryName">The name of the country.</param>
        /// <returns>The country code entity.</returns>
        public async Task<CountryCodes> GetCountryCodeByCountryNameAsync(string countryName)
        {
            return await _context.CountryCodes.FirstOrDefaultAsync(x => x.CountryName == countryName);
        }
    }
    
}
