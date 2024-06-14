using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    public class CarbonFlight
    {
        [BindNever]
        [ValidateNever]
        public required string type { get; set; }
        [Required]
        public required int passengers { get; set; }
        [Required]
        public required FlightLegs[] legs { get; set; }

        public string? distance_unit { get; set; }
    }
}
