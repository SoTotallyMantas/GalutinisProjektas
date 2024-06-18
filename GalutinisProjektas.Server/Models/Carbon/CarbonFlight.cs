using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace GalutinisProjektas.Server.Models.Carbon
{
    /// <summary>
    /// Represents flight-related parameters for carbon emissions calculation.
    /// </summary>
    [SwaggerSchema(Required = new[] { "Description" })]
    public class CarbonFlight
    {
        /// <summary>
        /// Gets or sets the type of flight.
        /// This property is not bound or validated during model binding.
        /// </summary>
        [BindNever]
        [ValidateNever]
        public required string type { get; set; }

        /// <summary>
        /// Gets or sets the number of passengers on the flight.
        /// </summary>
        [Required]
        [SwaggerSchema("The number of passengers on the flight")]
        public required int passengers { get; set; }

        /// <summary>
        /// Gets or sets the flight legs array.
        /// </summary>
        [Required]
        [SwaggerSchema("Flight Departure/Destination Array")]
        public required List<FlightLegs> legs { get; set; } = new List<FlightLegs>();

        /// <summary>
        /// Gets or sets the distance unit of the flight.
        /// </summary>
        [Required]
        [SwaggerSchema("The distance unit of the flight")]
        public required string distance_unit { get; set; }
    }
}
