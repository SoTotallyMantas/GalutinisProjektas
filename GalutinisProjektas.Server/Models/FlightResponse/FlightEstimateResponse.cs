using System.Text.Json.Serialization;
using GalutinisProjektas.Server.Models.UtilityModels;

namespace GalutinisProjektas.Server.Models.FlightResponse
{
    /// <summary>
    /// Represents the response structure for flight carbon emission estimates.
    /// </summary>
    public class FlightEstimateResponse
    {
        /// <summary>
        /// Gets or sets the main data containing flight carbon emission estimates.
        /// </summary>
        [JsonPropertyName("data")]
        public required FlightResponseData Data { get; set; }

        /// <summary>
        /// Gets or sets the collection of HATEOAS links associated with the response.
        /// </summary>
        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();

    }
}
