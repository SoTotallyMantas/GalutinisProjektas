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
        public DateTime EstimatedAt { get; set; }
        [JsonPropertyName("carbon_g")]
        public int CarbonG { get; set; }
        [JsonPropertyName("carbon_lb")]
        public double CarbonLb { get; set; }
        [JsonPropertyName("carbon_kg")]
        public double CarbonKg { get; set; }
        [JsonPropertyName("carbon_mt")]
        public double CarbonMt { get; set; }
        [JsonPropertyName("distance_unit")]
        public string DistanceUnit { get; set; }
        [JsonPropertyName("distance_value")]
        public double DistanceValue { get; set; }

    }
}
