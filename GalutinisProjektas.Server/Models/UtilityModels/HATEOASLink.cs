namespace GalutinisProjektas.Server.Models.UtilityModels
{
    /// <summary>
    /// Represents a HATEOAS (Hypermedia as the Engine of Application State) link.
    /// </summary>    
    public class HATEOASLink
    {
        /// <summary>
        /// Gets or sets the URL of the link.
        /// </summary>
        public required string Href { get; set; }

        /// <summary>
        /// Gets or sets the relationship of the link to the current resource.
        /// </summary>
        public required string Rel { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method used to interact with the link.
        /// </summary>
        public required string Method { get; set; }

    }
}
