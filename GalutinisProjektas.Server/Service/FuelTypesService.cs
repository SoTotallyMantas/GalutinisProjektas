using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.EntityFrameworkCore;

namespace GalutinisProjektas.Server.Service
{
    public class FuelTypesService
    {
        private readonly ModeldbContext _context;


        public FuelTypesService(ModeldbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FuelTypes>> GetFuelTypesAsync()
        {
            return await _context.FuelTypes.ToListAsync();
        }

        public async Task<FuelTypes> GetFuelTypeByIdAsync(int id)
        {
            return await _context.FuelTypes.FindAsync(id);
        }

        public async Task<IEnumerable<FuelTypes>> GetFuelTypeByNameAsync(string fuelType)
        {
            return await _context.FuelTypes.Where(x => x.FuelType == fuelType).ToListAsync();
        }
    }
}
