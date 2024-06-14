using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    public class FlightLegs
    {
        [Required]
        public required string departure_airport { get; set; }
        [Required]
        public required string destination_airport { get; set; }
    }
}
