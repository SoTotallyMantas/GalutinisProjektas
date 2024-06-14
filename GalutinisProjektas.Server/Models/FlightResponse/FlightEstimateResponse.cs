using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    public class FlightEstimateResponse
    {
        [JsonPropertyName("data")]
        public required FlightResponseData Data { get; set; }

        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();

    }
}
