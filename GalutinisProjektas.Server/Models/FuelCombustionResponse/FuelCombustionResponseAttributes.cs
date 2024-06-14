using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FuelCombustionResponse
{
    public class FuelCombustionResponseAttributes
    {
        [JsonPropertyName("fuel_source_type")]
        public string FuelSourceType { get; set; }
        [JsonPropertyName("fuel_source_unit")]
        public string FuelSourceUnit { get; set; }
        [JsonPropertyName("fuel_source_value")]
        public decimal FuelSourceValue { get; set; }

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

    }
}
