namespace GalutinisProjektas.Server.Models.AirPollution
{
    /// <summary>
    /// Represents the coordinates (latitude and longitude) for querying air pollution data.
    /// </summary>
    public class AirPollution
    {
        /// <summary>
        /// Gets or sets the latitude coordinate for querying air pollution data.
        /// </summary>
        public required decimal lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate for querying air pollution data.
        /// </summary>
        public required decimal lon { get; set; }
    }
}
