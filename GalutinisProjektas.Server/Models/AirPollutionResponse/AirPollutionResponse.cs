using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    public class AirPollutionResponse
    {
        [JsonPropertyName("coord")]
        public AirPollutionCoord Coord { get; set; }
        [JsonPropertyName("list")]
        public List<WeatherData> List { get; set; }

    }
}
