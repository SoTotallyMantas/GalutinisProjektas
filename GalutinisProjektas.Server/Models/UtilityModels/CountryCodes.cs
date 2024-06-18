using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalutinisProjektas.Server.Models.UtilityModels
{
    /// <summary>
    /// Represents a model class for storing country codes and names.
    /// </summary>
    public class CountryCodes
    {
        /// <summary>
        /// Gets or sets the unique identifier for the country code.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        [Required]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        [Required]
        public string CountryName { get; set; }
    }
}
