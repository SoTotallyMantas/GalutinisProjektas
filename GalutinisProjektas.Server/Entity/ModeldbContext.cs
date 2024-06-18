using Microsoft.EntityFrameworkCore;

namespace GalutinisProjektas.Server.Entity
{
    /// <summary>
    /// Represents the database context for the application's models.
    /// </summary>
    public class ModeldbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModeldbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public ModeldbContext(DbContextOptions<ModeldbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the database set for storing country codes.
        /// </summary>
        public DbSet<GalutinisProjektas.Server.Models.UtilityModels.CountryCodes> CountryCodes { get; set; }

        /// <summary>
        /// Gets or sets the database set for storing IATA codes.
        /// </summary>
        public DbSet<GalutinisProjektas.Server.Models.UtilityModels.IATACodes> IATACodes { get; set; }

        /// <summary>
        /// Gets or sets the database set for storing fuel types.
        /// </summary>
        public DbSet<GalutinisProjektas.Server.Models.UtilityModels.FuelTypes> FuelTypes { get; set; }



     

       }
    }
