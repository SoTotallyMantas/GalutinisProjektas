using GalutinisProjektas.Server.Models.UtilityModels;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.HATEOASResourceModels
{
    /// <summary>
    /// Represents a response model for country code information with HATEOAS links.
    /// </summary>
    public class CountryCodeResponse : CountryCodes
    {
        /// <summary>
        /// Gets or sets the collection of HATEOAS links related to the country code.
        /// </summary>
        [JsonPropertyOrder(4)]
        public List<HATEOASLink> Links { get; set; } 
    }
}
