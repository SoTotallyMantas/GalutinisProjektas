using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.EntityFrameworkCore;

namespace GalutinisProjektas.Server.Service
{
    /// <summary>
    /// Service class for handling operations related to fuel types.
    /// </summary>
    public class FuelTypesService
    {
        private readonly ModeldbContext _context;

        /// <summary>
        /// Constructor for FuelTypesService class.
        /// </summary>
        /// <param name="context">Database context instance.</param>
        public FuelTypesService(ModeldbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all fuel types asynchronously.
        /// </summary>
        /// <returns>A collection of all fuel types.</returns>
        public async Task<IEnumerable<FuelTypes>> GetFuelTypesAsync()
        {
            return await _context.FuelTypes.ToListAsync();
        }

        /// <summary>
        /// Retrieves a fuel type by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the fuel type.</param>
        /// <returns>The fuel type entity.</returns>
        public async Task<FuelTypes> GetFuelTypeByIdAsync(int id)
        {
            return await _context.FuelTypes.FindAsync(id);
        }

        /// <summary>
        /// Retrieves fuel types by their name asynchronously.
        /// </summary>
        /// <param name="fuelType">The name of the fuel type.</param>
        /// <returns>A collection of fuel type entities.</returns>
        public async Task<IEnumerable<FuelTypes>> GetFuelTypeByNameAsync(string fuelType)
        {
            return await _context.FuelTypes.Where(x => x.FuelType == fuelType).ToListAsync();
        }
    }
}
