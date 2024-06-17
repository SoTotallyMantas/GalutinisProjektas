using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    /// <summary>
    /// Represents the attributes of a flight estimate response.
    /// </summary>
    public class FlightResponseAttributes
    {
        /// <summary>
        /// Gets or sets the number of passengers on the flight.
        /// </summary>
        [JsonPropertyName("passengers")]
        public int Passengers { get; set; }

        /// <summary>
        /// Gets or sets the array of flight legs in the flight.
        /// </summary>
        [JsonPropertyName("legs")]
        public FlightLegResponse[] Legs { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the estimate was made.
        /// </summary>
        [JsonPropertyName("estimated_at")]
        public DateTime EstimatedAt { get; set; }

        /// <summary>
        /// Gets or sets the amount of carbon emissions in grams.
        /// </summary>
        [JsonPropertyName("carbon_g")]
        public int CarbonG { get; set; }

        /// <summary>
        /// Gets or sets the amount of carbon emissions in pounds.
        /// </summary>
        [JsonPropertyName("carbon_lb")]
        public double CarbonLb { get; set; }

        /// <summary>
        /// Gets or sets the amount of carbon emissions in kilograms.
        /// </summary>
        [JsonPropertyName("carbon_kg")]
        public double CarbonKg { get; set; }

        /// <summary>
        /// Gets or sets the amount of carbon emissions in metric tons.
        /// </summary>
        [JsonPropertyName("carbon_mt")]
        public double CarbonMt { get; set; }

        /// <summary>
        /// Gets or sets the unit of distance for the flight.
        /// </summary>
        [JsonPropertyName("distance_unit")]
        public string DistanceUnit { get; set; }

        /// <summary>
        /// Gets or sets the value of distance for the flight.
        /// </summary>
        [JsonPropertyName("distance_value")]
        public double DistanceValue { get; set; }

    }
}
