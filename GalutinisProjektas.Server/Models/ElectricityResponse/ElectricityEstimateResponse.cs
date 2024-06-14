using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.ElectricityResponse
{
    public class ElectricityEstimateResponse
    {
        [JsonPropertyName("data")]
        public required ElectricityResponseData Data { get; set; }

       
        public List<HATEOASLink> Links { get; set; } = new List<HATEOASLink>();
    }
}
