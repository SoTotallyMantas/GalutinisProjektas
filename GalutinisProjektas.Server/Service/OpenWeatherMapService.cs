using GalutinisProjektas.Server.Models.AirPollutionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;
using System.Text.Json;

namespace GalutinisProjektas.Server.Service
{
    /// <summary>
    /// Service class for interacting with the OpenWeatherMap API to retrieve air pollution data.
    /// </summary>
    public class OpenWeatherMapService(HttpClient httpClient, IConfiguration configuration, ILogger<OpenWeatherMapService> logger)
    {
        private readonly HttpClient _httpClient = httpClient;
#pragma warning disable CS8601 // Possible null reference assignment.
        private readonly string _apiKey = configuration["OpenWeatherMap:ApiKey"];  // API key is stored in appsettings.json
#pragma warning restore CS8601 // Possible null reference assignment.
        private readonly ILogger<OpenWeatherMapService> _logger = logger;

        /// <summary>
        /// Retrieves air pollution data from the OpenWeatherMap API based on latitude and longitude.
        /// </summary>
        /// <param name="latitude">Latitude of the location.</param>
        /// <param name="longitude">Longitude of the location.</param>
        /// <returns>A ServiceResponse containing the air pollution data for the specified location.</returns>
        public async Task<ServiceResponse<AirPollutionResponse>> GetAirPollutionDataAsync(double latitude,double longtitude)
        {

           string url = $"http://api.openweathermap.org/data/2.5/air_pollution?lat={latitude}&lon={longtitude}&appid={_apiKey}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to fetch data from OpenWeatherMap API. Status code: {response.StatusCode}");
                    return new ServiceResponse<AirPollutionResponse>
                    {
                        StatusCode = (int)response.StatusCode,
                        ErrorMessage = $"Failed to fetch data from OpenWeatherMap API. Status code: {response.StatusCode}"
                    };
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var airPollutionResponse = JsonSerializer.Deserialize<AirPollutionResponse>(jsonResponse);

                return new ServiceResponse<AirPollutionResponse>
                {
                    Data = airPollutionResponse,
                    StatusCode = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while fetching data from OpenWeatherMap API: {ex.Message}");
                return new ServiceResponse<AirPollutionResponse>
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal server error"
                };
            }
        }}

    }

