using System.Text.Json.Serialization;
using GalutinisProjektas.Server.Models.UtilityModels;

namespace GalutinisProjektas.Server.Models.FuelCombustionResponse
{
    public class FuelCumbustionEstimateResponse
    {
        [JsonPropertyName("data")]
        public required FuelCombustionResponseData Data { get; set; }

        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();
    }
}
