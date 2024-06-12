using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    public class AirPollutionComponents
    {
        [JsonPropertyName("co")]
        public double co { get; set; }

        [JsonPropertyName("no")]
        public double no { get; set; }

        [JsonPropertyName("no2")]
        public double no2 { get; set; }

        [JsonPropertyName("o3")]
        public double o3 { get; set; }

        [JsonPropertyName("so2")]
        public double so2 { get; set; }

        [JsonPropertyName("pm2_5")]
        public double pm2_5 { get; set; }

        [JsonPropertyName("pm10")]
        public double pm10 { get; set; }

        [JsonPropertyName("nh3")]
        public double nh3 { get; set; }
    }
}
