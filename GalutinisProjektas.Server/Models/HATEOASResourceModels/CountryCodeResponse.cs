using GalutinisProjektas.Server.Models.UtilityModels;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.HATEOASResourceModels
{
    public class CountryCodeResponse : CountryCodes
    {
        [JsonPropertyOrder(4)]
        public List<HATEOASLink> Links { get; set; } 
    }
}
