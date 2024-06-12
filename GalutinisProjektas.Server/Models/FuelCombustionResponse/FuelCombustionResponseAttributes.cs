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
        public string EstimatedAt { get; set; }

        [JsonPropertyName("carbon_g")]
        public int CarbonG { get; set; }
        [JsonPropertyName("carbon_lb")]
            
        public int CarbonLb { get; set; }
        [JsonPropertyName("carbon_kg")]
        public int CarbonKg { get; set; }
        [JsonPropertyName("carbon_mt")]
        public int CarbonMt { get; set; }

    }
}
