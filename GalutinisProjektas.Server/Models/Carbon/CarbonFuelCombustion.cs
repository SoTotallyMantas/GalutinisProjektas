using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    /// <summary>
    /// Represents parameters for calculating carbon emissions from fuel combustion.
    /// </summary>
    public class CarbonFuelCombustion
    {
        /// <summary>
        /// Gets or sets the type of fuel combustion.
        /// This property is not bound or validated during model binding.
        /// </summary>
        [BindNever]
        [ValidateNever]
        public required string type { get; set; }

        /// <summary>
        /// Gets or sets the type of fuel source.
        /// </summary>
        [Required]
        [SwaggerSchema("The fuel source type")]
        public required string fuel_source_type { get; set; }

        /// <summary>
        /// Gets or sets the unit of the fuel source.
        /// </summary>
        [Required]
        [SwaggerSchema("The fuel source unit")]
        public required string fuel_source_unit { get; set; }

        /// <summary>
        /// Gets or sets the value of the fuel source.
        /// </summary>
        [Required]
        [SwaggerSchema("The fuel source value")]
        public decimal fuel_source_value { get; set; }

    }
}
