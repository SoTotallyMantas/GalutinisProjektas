using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.ElectricityResponse
{
    /// <summary>
    /// Represents the data structure of an electricity consumption response.
    /// </summary>
    public class ElectricityResponseData
    {
        /// <summary>
        /// Gets or sets the identifier of the electricity consumption data.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the electricity consumption data.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the attributes of the electricity consumption data.
        /// </summary>
        [JsonPropertyName("attributes")]
        public ElectricityResponseAttributes Attributes { get; set; }

    }
}
