using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.UtilityModels
{
    /// <summary>
    /// Represents a model class for storing fuel types.
    /// </summary>
    public class FuelTypes
    {
        /// <summary>
        /// Gets or sets the unique identifier for the fuel type.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the fuel.
        /// </summary>
        [Required]
        public string FuelName { get; set; }

        /// <summary>
        /// Gets or sets the type of the fuel.
        /// </summary>
        [Required]
        public string FuelType { get; set; }

        /// <summary>
        /// Gets or sets the unit of measurement for the fuel.
        /// </summary>
        [Required]
        public string FuelUnit { get; set; }

    }
}
