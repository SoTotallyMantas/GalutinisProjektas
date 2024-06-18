using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.ElectricityResponse
{
    /// <summary>
    /// Represents the response structure for electricity estimation.
    /// </summary>
    public class ElectricityEstimateResponse
    {
        /// <summary>
        /// Gets or sets the electricity response data.
        /// </summary>
        [JsonPropertyName("data")]
        public required ElectricityResponseData Data { get; set; }

        /// <summary>
        /// Gets or sets the HATEOAS links related to the electricity estimate.
        /// </summary>
        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();
    }
}
