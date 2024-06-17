using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    /// <summary>
    /// Represents the data structure of a flight estimate response.
    /// </summary>
    public class FlightResponseData
    {
        /// <summary>
        /// Gets or sets the ID associated with the flight estimate data.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the flight estimate response data.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the attributes associated with the flight estimate response.
        /// </summary>
        [JsonPropertyName("attributes")]
        public FlightResponseAttributes Attributes { get; set; }

    }
}
