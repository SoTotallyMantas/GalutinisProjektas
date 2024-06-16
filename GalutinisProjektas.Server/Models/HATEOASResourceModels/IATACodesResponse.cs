using GalutinisProjektas.Server.Models.UtilityModels;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.HATEOASResourceModels
{
    public class IATACodesResponse : IATACodes
    {
        [JsonPropertyOrder(6)]
        public List<HATEOASLink> Links { get; set; }

    }
}
