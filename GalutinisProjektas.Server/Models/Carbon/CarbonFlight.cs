namespace GalutinisProjektas.Server.Models.Carbon
{
    public class CarbonFlight
    {
        public required string type { get; set; }
        public required int passengers { get; set; }

        public required FlightLegs[] legs { get; set; }

        public string? distance_unit { get; set; }
    }
}
