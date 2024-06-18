using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FuelCombustionResponse
{
    /// <summary>
    /// Represents the attributes associated with a fuel combustion response.
    /// </summary>
    public class FuelCombustionResponseAttributes
    {
        /// <summary>
        /// Gets or sets the type of the fuel source.
        /// </summary>
        [JsonPropertyName("fuel_source_type")]
        public string FuelSourceType { get; set; }

        /// <summary>
        /// Gets or sets the unit of measurement for the fuel source.
        /// </summary>
        [JsonPropertyName("fuel_source_unit")]
        public string FuelSourceUnit { get; set; }

        /// <summary>
        /// Gets or sets the value of the fuel source.
        /// </summary>
        [JsonPropertyName("fuel_source_value")]
        public decimal FuelSourceValue { get; set; }
        
        /// <summary>
        /// Gets or sets the timestamp when the estimate was made.
        /// </summary>
        [JsonPropertyName("estimated_at")]
        public DateTime EstimatedAt { get; set; }

        /// <summary>
        /// Gets or sets the carbon emissions in grams.
        /// </summary>
        [JsonPropertyName("carbon_g")]
        public int CarbonG { get; set; }
        
        /// <summary>
        /// Gets or sets the carbon emissions in pounds.
        /// </summary>
        [JsonPropertyName("carbon_lb")]
        public double CarbonLb { get; set; }

        /// <summary>
        /// Gets or sets the carbon emissions in kilograms.
        /// </summary>
        [JsonPropertyName("carbon_kg")]
        public double CarbonKg { get; set; }

        /// <summary>
        /// Gets or sets the carbon emissions in metric tons.
        /// </summary>
        [JsonPropertyName("carbon_mt")]
        public double CarbonMt { get; set; }

    }
}
