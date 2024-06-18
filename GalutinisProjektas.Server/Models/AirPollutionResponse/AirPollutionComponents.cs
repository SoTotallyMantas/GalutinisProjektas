using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    /// <summary>
    /// Represents the components of air pollution with their respective values.
    /// </summary>
    public class AirPollutionComponents
    {
        /// <summary>
        /// Gets or sets the concentration of carbon monoxide (CO) in the air.
        /// </summary>
        [JsonPropertyName("co")]
        public double co { get; set; }

        /// <summary>
        /// Gets or sets the concentration of nitrogen monoxide (NO) in the air.
        /// </summary>
        [JsonPropertyName("no")]
        public double no { get; set; }

        /// <summary>
        /// Gets or sets the concentration of nitrogen dioxide (NO2) in the air.
        /// </summary>
        [JsonPropertyName("no2")]
        public double no2 { get; set; }

        /// <summary>
        /// Gets or sets the concentration of ozone (O3) in the air.
        /// </summary>
        [JsonPropertyName("o3")]
        public double o3 { get; set; }

        /// <summary>
        /// Gets or sets the concentration of sulfur dioxide (SO2) in the air.
        /// </summary>
        [JsonPropertyName("so2")]
        public double so2 { get; set; }

        /// <summary>
        /// Gets or sets the concentration of fine particles (PM2.5) in the air.
        /// </summary>
        [JsonPropertyName("pm2_5")]
        public double pm2_5 { get; set; }

        /// <summary>
        /// Gets or sets the concentration of coarse particles (PM10) in the air.
        /// </summary>
        [JsonPropertyName("pm10")]
        public double pm10 { get; set; }

        /// <summary>
        /// Gets or sets the concentration of ammonia (NH3) in the air.
        /// </summary>
        [JsonPropertyName("nh3")]
        public double nh3 { get; set; }
    }
}
