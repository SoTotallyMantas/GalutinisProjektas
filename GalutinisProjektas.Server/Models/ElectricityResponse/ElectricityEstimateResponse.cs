using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.ElectricityResponse
{
    public class ElectricityEstimateResponse
    {
        [JsonPropertyName("data")]
        public ElectricityResponseData Data { get; set; }
    }
}
