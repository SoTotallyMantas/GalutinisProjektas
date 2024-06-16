using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    public class FlightLegs
    {
        [Required]
        [SwaggerSchema("The departure airport of the flight")]
        public required string departure_airport { get; set; }
        [Required]
        [SwaggerSchema("The destination airport of the flight")]
        public required string destination_airport { get; set; }
    }
}
