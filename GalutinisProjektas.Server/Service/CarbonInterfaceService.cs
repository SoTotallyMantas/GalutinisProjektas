using GalutinisProjektas.Server.Models.Carbon;
using GalutinisProjektas.Server.Models.ElectricityResponse;
using GalutinisProjektas.Server.Models.FlightResponse;
using GalutinisProjektas.Server.Models.FuelCombustionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace GalutinisProjektas.Server.Service
{
    public class CarbonInterfaceService : ICarbonInterfaceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CarbonInterfaceService> _logger;
        private readonly string _apiKey;
        private readonly string baseURL = "https://www.carboninterface.com/api/v1/estimates";

        public CarbonInterfaceService(HttpClient httpClient, ILogger<CarbonInterfaceService> logger, IConfiguration configuration)
        {
            _apiKey = configuration["CarbonInterface:ApiKey"];
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            _logger = logger;
           

        }
        public async Task<ServiceResponse<FuelCumbustionEstimateResponse>> GetFuelEstimateAsync(CarbonFuelCombustion request)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(request);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var httpResponse = await _httpClient.PostAsync(baseURL, httpContent);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var fuelCumbustionEstimateResponse = JsonSerializer.Deserialize<FuelCumbustionEstimateResponse>(jsonResponse);

                    return new ServiceResponse<FuelCumbustionEstimateResponse>
                    {
                        Data = fuelCumbustionEstimateResponse,
                        StatusCode = (int)httpResponse.StatusCode
                    };
                }
                else
                {
                    var jsonErrorResponse = await httpResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to fetch data from Carbon Interface API. Status code: {httpResponse.StatusCode}, {jsonErrorResponse}");
                    return new ServiceResponse<FuelCumbustionEstimateResponse>
                    {
                        StatusCode = (int)httpResponse.StatusCode,
                        ErrorMessage = $"Failed to fetch data from Carbon Interface API. Status code: {httpResponse.StatusCode}"
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ServiceResponse<FuelCumbustionEstimateResponse>
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal server error"
                };
            }

        }

        public async Task<ServiceResponse<FlightEstimateResponse>> GetFlightEstimateAsync(CarbonFlight request)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(request);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var httpResponse = await _httpClient.PostAsync(baseURL, httpContent);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var flightEstimateResponse = JsonSerializer.Deserialize<FlightEstimateResponse>(jsonResponse);

                    return new ServiceResponse<FlightEstimateResponse>
                    {
                        Data = flightEstimateResponse,
                        StatusCode = (int)httpResponse.StatusCode
                    };
                }
                else
                {
                    var jsonErrorResponse = await httpResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to fetch data from Carbon Interface API. Status code: {httpResponse.StatusCode}, {jsonErrorResponse}");
                    return new ServiceResponse<FlightEstimateResponse>
                    {
                        StatusCode = (int)httpResponse.StatusCode,
                        ErrorMessage = $"Failed to fetch data from Carbon Interface API. Status code: {httpResponse.StatusCode})"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ServiceResponse<FlightEstimateResponse>
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal server error"
                };

            }
        }

        public async Task<ServiceResponse<ElectricityEstimateResponse>> GetElectricityEstimateAsync(CarbonElectricity request)
        {
            try
            {


                
                var jsonContent = JsonSerializer.Serialize(request);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
               

                var httpResponse = await _httpClient.PostAsync(baseURL,httpContent);


                if (httpResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var electricityEstimateResponse = JsonSerializer.Deserialize<ElectricityEstimateResponse>(jsonResponse);

                    return new ServiceResponse<ElectricityEstimateResponse>
                    {
                        Data = electricityEstimateResponse,
                        StatusCode = (int)httpResponse.StatusCode
                    };
                }
                else
                {
                    var jsonErrorResponse = await httpResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to fetch data from Carbon Interface API. Status code: {httpResponse.StatusCode},{jsonErrorResponse}");
                  
                    return new ServiceResponse<ElectricityEstimateResponse>
                    {
                        StatusCode = (int)httpResponse.StatusCode,
                        
                        ErrorMessage = $"Failed to fetch data from Carbon Interface API. Status code: {httpResponse.StatusCode}"
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ServiceResponse<ElectricityEstimateResponse>
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal server error"
                };

            }
        }
    }
}
