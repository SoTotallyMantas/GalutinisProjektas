using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.Carbon
{
    [SwaggerSchema(Required = new[] { "Description" })]
    public class CarbonElectricity
    {
        [BindNever]
        [ValidateNever]
        public required string type { get; set; }
        [Required]
        [SwaggerSchema("The electricity unit")]
        public required string electricity_unit { get; set; }
        [Required]
        [SwaggerSchema("The electricity value")]
        public required decimal electricity_value { get; set; }
        [Required]
        [SwaggerSchema("The country of the electricity")]
        public required string country { get; set; }
   
    }
}
