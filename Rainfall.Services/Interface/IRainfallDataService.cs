using Rainfall.Data;
using Rainfall.Data.Interface;
using Rainfall.Data.Interfaces;

namespace Rainfall.Services.Interface
{
    public interface IRainfallDataService
    {
        Task<IRainfallResponse> GetRainfallDataAsync(string stationId);
    }
}