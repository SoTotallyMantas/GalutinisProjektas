namespace GalutinisProjektas.Server.Models.UtilityModels
{
    /// <summary>
    /// Represents a generic service response containing data of type T, status code, and optional error message.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the response.</typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Gets or sets the data returned by the service operation.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Gets or sets the status code indicating the result of the service operation.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error message if the service operation encountered an error.
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
