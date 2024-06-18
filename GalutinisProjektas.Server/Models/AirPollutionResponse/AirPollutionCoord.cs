using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    /// <summary>
    /// Represents the coordinates (longitude and latitude) associated with air pollution data.
    /// </summary>
    public class AirPollutionCoord
    {
        /// <summary>
        /// Gets or sets the longitude coordinate.
        /// </summary>
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate.
        /// </summary>
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
      
    }
}
