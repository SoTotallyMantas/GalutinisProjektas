using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    /// <summary>
    /// Represents a flight leg with departure and destination airports in a flight response.
    /// </summary>
    public class FlightLegResponse
    {
        /// <summary>
        /// Gets or sets the departure airport code or name.
        /// </summary>
        [JsonPropertyName("departure_airport")]
        public string DepartureAirport { get; set; }

        /// <summary>
        /// Gets or sets the destination airport code or name.
        /// </summary>
        [JsonPropertyName("destination_airport")]
        public string DestinationAirport { get; set; }

    }
}
