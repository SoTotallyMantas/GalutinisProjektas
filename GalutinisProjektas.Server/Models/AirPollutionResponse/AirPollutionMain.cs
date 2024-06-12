using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    public class AirPollutionMain
    {
        [JsonPropertyName("aqi")]
        public double Aqi { get; set; }
    }
}
