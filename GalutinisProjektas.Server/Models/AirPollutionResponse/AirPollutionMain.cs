using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    /// <summary>
    /// Represents the main air pollution data including Air Quality Index (AQI).
    /// </summary>
    public class AirPollutionMain
    {
        /// <summary>
        /// Gets or sets the Air Quality Index (AQI) value.
        /// </summary>
        [JsonPropertyName("aqi")]
        public double Aqi { get; set; }
    }
}
