using GalutinisProjektas.Server.Models.UtilityModels;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.HATEOASResourceModels
{
    /// <summary>
    /// Represents a response model for fuel type information with HATEOAS links.
    /// </summary>
    public class FuelTypesResponse : FuelTypes
    {
        /// <summary>
        /// Gets or sets the collection of HATEOAS links related to the fuel type.
        /// </summary>
        [JsonPropertyOrder(5)]
        public List<HATEOASLink> Links { get; set; }

    }
}
