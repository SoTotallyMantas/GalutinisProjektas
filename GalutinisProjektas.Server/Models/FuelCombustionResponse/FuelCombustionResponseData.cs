using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FuelCombustionResponse
{
    public class FuelCombustionResponseData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("attributes")]
        public FuelCombustionResponseAttributes Attributes { get; set; }
    }
}
