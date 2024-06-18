using System.Text.Json.Serialization;
using System.Collections.Generic;
using GalutinisProjektas.Server.Models.UtilityModels;
namespace GalutinisProjektas.Server.Models.AirPollutionResponse
{
    /// <summary>
    /// Represents the response containing air pollution data.
    /// </summary>
    public class AirPollutionResponse
    {
        /// <summary>
        /// Gets or sets the geographical coordinates associated with the air pollution data.
        /// </summary>
        [JsonPropertyName("coord")]
        public required AirPollutionCoord Coord { get; set; }

        /// <summary>
        /// Gets or sets the list of weather data entries associated with the air pollution.
        /// </summary>
        [JsonPropertyName("list")]
        public required List<WeatherData> List { get; set; }

        /// <summary>
        /// Gets or sets the collection of HATEOAS links associated with the air pollution response.
        /// </summary>
        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();

        
    }
}

