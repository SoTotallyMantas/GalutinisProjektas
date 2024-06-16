using GalutinisProjektas.Server.Models.UtilityModels;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.HATEOASResourceModels
{
    public class FuelTypesResponse : FuelTypes
    {
        [JsonPropertyOrder(5)]
        public List<HATEOASLink> Links { get; set; }

    }
}
