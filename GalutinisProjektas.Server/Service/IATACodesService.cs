using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.EntityFrameworkCore;

namespace GalutinisProjektas.Server.Service
{
    public class IATACodesService
    {
        private readonly ModeldbContext _context;

        public IATACodesService(ModeldbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IATACodes>> GetIATACodesAsync()
        {
            return await _context.IATACodes.AsNoTracking().ToListAsync();
        }

        public async Task<IATACodes> GetIATACodeByIdAsync(int id)
        {
            return await _context.IATACodes.FindAsync(id);
        }

        public async Task<IATACodes> GetIATACodeByCodeAsync(string IATA)
        {
            return await _context.IATACodes.FirstOrDefaultAsync(x => x.IATA == IATA);
        }
    }
}

