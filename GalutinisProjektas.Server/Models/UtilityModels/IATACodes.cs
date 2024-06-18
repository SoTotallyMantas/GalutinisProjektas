using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.UtilityModels
{
    /// <summary>
    /// Represents IATA (International Air Transport Association) codes for airports.
    /// </summary>
    public class IATACodes
    {
        /// <summary>
        /// Gets or sets the unique identifier of the IATA code entry.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the IATA code.
        /// </summary>
        [Required]
        public string IATA { get; set; }

        /// <summary>
        /// Gets or sets the name of the airport.
        /// </summary>
        [Required]
        public string AirportName { get; set; }

        /// <summary>
        /// Gets or sets the city where the airport is located.
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country where the airport is located.
        /// </summary>
        [Required]
        public string Country { get; set; }
    }
}
