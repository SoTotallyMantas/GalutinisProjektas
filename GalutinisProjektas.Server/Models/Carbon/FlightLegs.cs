using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    /// <summary>
    /// Represents the departure and destination airports of a flight leg.
    /// </summary>
    public class FlightLegs
    {
        /// <summary>
        /// Gets or sets the departure airport code.
        /// </summary>
        [Required]
        [SwaggerSchema("The departure airport of the flight")]
        public required string departure_airport { get; set; }

        /// <summary>
        /// Gets or sets the destination airport code.
        /// </summary>
        [Required]
        [SwaggerSchema("The destination airport of the flight")]
        public required string destination_airport { get; set; }
    }
}
