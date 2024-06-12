using System.Runtime.InteropServices;

namespace GalutinisProjektas.Server.Models.Carbon
{
    public class CarbonElectricity
    {
        public required string type { get; set; }
        public string? electricity_unit { get; set; }
        public required decimal electricity_value { get; set; }
        public required string country { get; set; }
        public string? state { get; set; }
    }
}
