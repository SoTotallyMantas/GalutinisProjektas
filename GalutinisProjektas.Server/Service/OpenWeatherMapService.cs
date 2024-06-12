using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Models.AirPollutionResponse;
using System.Text.Json;

namespace GalutinisProjektas.Server.Service
{
    public class OpenWeatherMapService(HttpClient httpClient, IConfiguration configuration, ILogger<OpenWeatherMapService> logger)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _apiKey = configuration["OpenWeatherMap:ApiKey"];  // API key is stored in appsettings.json
        private readonly ILogger<OpenWeatherMapService> _logger = logger;


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

