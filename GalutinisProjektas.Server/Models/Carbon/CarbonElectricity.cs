using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace GalutinisProjektas.Server.Models.Carbon
{
    public class CarbonElectricity
    {
        [BindNever]
        [ValidateNever]
        public required string type { get; set; }
        [Required]
        public required string electricity_unit { get; set; }
        [Required]
        public required decimal electricity_value { get; set; }
        [Required]
        public required string country { get; set; }
        public string? state { get; set; }
    }
}
