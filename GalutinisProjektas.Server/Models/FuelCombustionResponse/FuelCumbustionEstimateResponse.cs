using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FuelCombustionResponse
{
    public class FuelCumbustionEstimateResponse
    {
        [JsonPropertyName("data")]
        public FuelCombustionResponseData Data { get; set; }
    }
}
