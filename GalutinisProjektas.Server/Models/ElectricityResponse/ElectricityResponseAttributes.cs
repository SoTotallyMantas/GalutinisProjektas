using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.ElectricityResponse
{
    /// <summary>
    /// Represents the attributes of an electricity consumption response.
    /// </summary>
    public class ElectricityResponseAttributes
    {
        /// <summary>
        /// Gets or sets the country of the electricity consumption.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the unit of electricity consumption.
        /// </summary>
        [JsonPropertyName("electricity_unit")]
        public string ElectricityUnit { get; set; }

        /// <summary>
        /// Gets or sets the value of electricity consumption.
        /// </summary>
        [JsonPropertyName("electricity_value")]
        public decimal ElectricityValue { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the estimation was made.
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
