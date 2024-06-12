using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    public class FlightResponseData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("attributes")]
        public FlightResponseAttributes Attributes { get; set; }

    }
}
