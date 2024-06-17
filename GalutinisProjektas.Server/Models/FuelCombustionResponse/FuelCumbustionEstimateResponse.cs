using System.Text.Json.Serialization;
using GalutinisProjektas.Server.Models.UtilityModels;

namespace GalutinisProjektas.Server.Models.FuelCombustionResponse
{
    /// <summary>
    /// Represents the response structure for fuel combustion estimates.
    /// </summary>
    public class FuelCumbustionEstimateResponse
    {
        /// <summary>
        /// Gets or sets the fuel combustion data.
        /// </summary>
        [JsonPropertyName("data")]
        public required FuelCombustionResponseData Data { get; set; }

        /// <summary>
        /// Gets or sets the collection of HATEOAS links related to the fuel combustion response.
        /// </summary>
        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();
    }
}
