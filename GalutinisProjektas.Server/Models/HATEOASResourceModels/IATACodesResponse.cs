using GalutinisProjektas.Server.Models.UtilityModels;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.HATEOASResourceModels
{
    /// <summary>
    /// Represents a response model for IATA codes with HATEOAS links.
    /// </summary>
    public class IATACodesResponse : IATACodes
    {
        /// <summary>
        /// Gets or sets the collection of HATEOAS links related to the IATA code.
        /// </summary>
        [JsonPropertyOrder(6)]
        public List<HATEOASLink> Links { get; set; }

    }
}
