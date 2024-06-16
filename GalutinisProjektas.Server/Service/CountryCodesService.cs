using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models.HATEOASResourceModels;
using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.EntityFrameworkCore;


namespace GalutinisProjektas.Server.Service
{
    public class CountryCodesService
    {
        private readonly ModeldbContext _context;
        public CountryCodesService(ModeldbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CountryCodes>> GetAllCountryCodesAsync()
        {
            return await _context.CountryCodes.ToListAsync();
        }
        public async Task<CountryCodes> GetCountryCodeByIdAsync(int id)
        {
            return await _context.CountryCodes.FindAsync(id);
        }
        public async Task<CountryCodes> GetCountryCodeByCountryNameAsync(string countryName)
        {
            return await _context.CountryCodes.FirstOrDefaultAsync(x => x.CountryName == countryName);
        }
    }
    
}
