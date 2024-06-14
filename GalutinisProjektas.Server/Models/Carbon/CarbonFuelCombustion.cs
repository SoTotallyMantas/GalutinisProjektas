using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    public class CarbonFuelCombustion
    {
        [BindNever]
        [ValidateNever]
        public required string type { get; set; }
        [Required]
        public required string fuel_source_type { get; set; }
        [Required]

        public required string fuel_source_unit { get; set; }

        public decimal fuel_source_value { get; set; }

    }
}
