using GalutinisProjektas.Server.Models.AirPollutionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;

namespace GalutinisProjektas.Server.Service
{
    public interface IOpenWeatherMapService
    {
        Task<ServiceResponse<AirPollutionResponse>> GetAirPollutionDataAsync(double latitude, double longitude);
    }
}
