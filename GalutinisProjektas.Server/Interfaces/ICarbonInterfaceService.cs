using GalutinisProjektas.Server.Models.Carbon;
using GalutinisProjektas.Server.Models.ElectricityResponse;
using GalutinisProjektas.Server.Models.FlightResponse;
using GalutinisProjektas.Server.Models.FuelCombustionResponse;
using GalutinisProjektas.Server.Models.UtilityModels;

namespace GalutinisProjektas.Server.Interface
{
    public interface ICarbonInterfaceService
    {
        Task<ServiceResponse<ElectricityEstimateResponse>> GetElectricityEstimateAsync(CarbonElectricity request);
        Task<ServiceResponse<FlightEstimateResponse>> GetFlightEstimateAsync(CarbonFlight request);
        Task<ServiceResponse<FuelCumbustionEstimateResponse>> GetFuelEstimateAsync(CarbonFuelCombustion request);
    }

}
