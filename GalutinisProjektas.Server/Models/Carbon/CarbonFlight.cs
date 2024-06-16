using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    [SwaggerSchema(Required = new[] { "Description" })]
    public class CarbonFlight
    {
        
        [BindNever]
        [ValidateNever]
       
        public required string type { get; set; }
        [Required]
        [SwaggerSchema("The number of passengers on the flight")]
        public required int passengers { get; set; }
        [Required]
        [SwaggerSchema("Flight Departure/Destination Array")]
        public required FlightLegs[] legs { get; set; }

        [Required]
        [SwaggerSchema("The distance unit of the flight")]
        public required string distance_unit { get; set; }
    }
}
