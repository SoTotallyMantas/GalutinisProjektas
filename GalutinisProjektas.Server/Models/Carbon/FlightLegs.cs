namespace GalutinisProjektas.Server.Models.Carbon
{
    public class FlightLegs
    {
        public required string departure_airport { get; set; }
        public required string arrival_airport { get; set; }
        public string? cabin_class { get; set; }
    }
}
