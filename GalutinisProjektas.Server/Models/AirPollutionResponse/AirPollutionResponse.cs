using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    public class AirPollutionResponse
    {
        [JsonPropertyName("coord")]
        public AirPollutionCoord Coord { get; set; }
        [JsonPropertyName("list")]
        public List<WeatherData> List { get; set; }

            public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();

        
    }
}

