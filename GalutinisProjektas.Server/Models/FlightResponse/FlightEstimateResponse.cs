using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    public class FlightEstimateResponse
    {
        [JsonPropertyName("data")]
        public FlightResponseData Data { get; set; }

    }
}
