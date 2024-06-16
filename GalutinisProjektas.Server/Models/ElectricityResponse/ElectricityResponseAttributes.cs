using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.ElectricityResponse
{
    public class ElectricityResponseAttributes
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }
        
        [JsonPropertyName("electricity_unit")]
        public string ElectricityUnit { get; set; }
        [JsonPropertyName("electricity_value")]
        public decimal ElectricityValue { get; set; }

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
