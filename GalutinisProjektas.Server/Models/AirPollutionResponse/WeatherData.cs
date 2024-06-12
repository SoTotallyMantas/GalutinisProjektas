using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    public class WeatherData
    {
        [JsonPropertyName("dt")]
        public long Dt { get; set; }
        [JsonPropertyName("main")]
        public AirPollutionMain Main { get; set; }

        [JsonPropertyName("components")]
        public AirPollutionComponents Components { get; set; }

    }
}
