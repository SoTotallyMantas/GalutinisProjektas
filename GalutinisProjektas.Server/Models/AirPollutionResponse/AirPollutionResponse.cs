using System.Text.Json.Serialization;
using System.Collections.Generic;
using GalutinisProjektas.Server.Models.UtilityModels;
namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    public class AirPollutionResponse
    {
        [JsonPropertyName("coord")]
        public required AirPollutionCoord Coord { get; set; }
        [JsonPropertyName("list")]
        public required List<WeatherData> List { get; set; }

        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();

        
    }
}

