using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.Carbon
{
    /// <summary>
    /// Represents electricity-related parameters for carbon emissions calculation.
    /// </summary>
    [SwaggerSchema(Required = new[] { "Description" })]
    public class CarbonElectricity
    {
        /// <summary>
        /// Gets or sets the type of electricity.
        /// This property is not bound or validated during model binding.
        /// </summary>
        [BindNever]
        [ValidateNever]
        public required string type { get; set; }

        /// <summary>
        /// Gets or sets the electricity unit.
        /// </summary>
        [Required]
        [SwaggerSchema("The electricity unit")]
        public required string electricity_unit { get; set; }

        /// <summary>
        /// Gets or sets the electricity value.
        /// </summary>
        [Required]
        [SwaggerSchema("The electricity value")]
        public required decimal electricity_value { get; set; }

        /// <summary>
        /// Gets or sets the country of the electricity.
        /// </summary>
        [Required]
        [SwaggerSchema("The country of the electricity")]
        public required string country { get; set; }
   
    }
}
