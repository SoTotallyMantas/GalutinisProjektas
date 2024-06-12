using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    public class FlightResponseAttributes
    {
        [JsonPropertyName("passengers")]
        public int Passengers { get; set; }
        [JsonPropertyName("legs")]
        public FlightLegResponse[] Legs { get; set; }
        [JsonPropertyName("estimated_at")]
        public string EstimatedAt { get; set; }
        [JsonPropertyName("carbon_g")]
        public int CarbonG { get; set; }
        [JsonPropertyName("carbon_lb")]
        public int CarbonLb { get; set; }
        [JsonPropertyName("carbon_kg")]
        public int CarbonKg { get; set; }
        [JsonPropertyName("carbon_mt")]
        public int CarbonMt { get; set; }
        [JsonPropertyName("distance_unit")]
        public string DistanceUnit { get; set; }
        [JsonPropertyName("distance_value")]
        public int DistanceValue { get; set; }

    }
}
