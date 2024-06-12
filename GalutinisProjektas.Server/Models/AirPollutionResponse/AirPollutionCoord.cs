using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    public class AirPollutionCoord
    {

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
      
    }
}
