using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.FuelCombustionResponse
{
    /// <summary>
    /// Represents the data structure for fuel combustion response.
    /// </summary>
    public class FuelCombustionResponseData
    {
        /// <summary>
        /// Gets or sets the identifier of the fuel combustion data.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the fuel combustion data.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the attributes associated with the fuel combustion data.
        /// </summary>
        [JsonPropertyName("attributes")]
        public FuelCombustionResponseAttributes Attributes { get; set; }
    }
}
