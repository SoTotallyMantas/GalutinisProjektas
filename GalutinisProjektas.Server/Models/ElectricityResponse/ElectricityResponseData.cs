using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.ElectricityResponse
{
    public class ElectricityResponseData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("attributes")]
        public ElectricityResponseAttributes Attributes { get; set; }

    }
}
