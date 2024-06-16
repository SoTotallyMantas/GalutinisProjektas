using Microsoft.EntityFrameworkCore;

namespace GalutinisProjektas.Server.Entity
{
    public class ModeldbContext : DbContext
    {
        public ModeldbContext(DbContextOptions<ModeldbContext> options) : base(options)
        {
        }

        public DbSet<GalutinisProjektas.Server.Models.UtilityModels.CountryCodes> CountryCodes { get; set; }
        public DbSet<GalutinisProjektas.Server.Models.UtilityModels.IATACodes> IATACodes { get; set; }
        public DbSet<GalutinisProjektas.Server.Models.UtilityModels.FuelTypes> FuelTypes { get; set; }



     

       }
    }
