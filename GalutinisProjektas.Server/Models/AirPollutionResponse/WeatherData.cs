using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    /// <summary>
    /// Represents weather data associated with air pollution.
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Gets or sets the timestamp of the weather data.
        /// </summary>
        [JsonPropertyName("dt")]
        public long Dt { get; set; }

        /// <summary>
        /// Gets or sets the main air pollution parameters.
        /// </summary>
        [JsonPropertyName("main")]
        public AirPollutionMain Main { get; set; }

        /// <summary>
        /// Gets or sets the components of air pollution.
        /// </summary>
        [JsonPropertyName("components")]
        public AirPollutionComponents Components { get; set; }

    }
}
