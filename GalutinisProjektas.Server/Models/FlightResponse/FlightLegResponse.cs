using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    public class FlightLegResponse
    {
        [JsonPropertyName("departure_airport")]
        public string DepartureAirport { get; set; }
        [JsonPropertyName("destination_airport")]
        public string DestinationAirport { get; set; }

    }
}
