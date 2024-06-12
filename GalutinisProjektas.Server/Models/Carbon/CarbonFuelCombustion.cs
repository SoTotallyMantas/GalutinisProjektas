namespace GalutinisProjektas.Server.Models.Carbon
{
    public class CarbonFuelCombustion
    {
        public required string type { get; set; }
        public required string fuel_source_type { get; set; }

        public required string fuel_source_unit { get; set; }

        public decimal fuel_source_value { get; set; }

    }
}
