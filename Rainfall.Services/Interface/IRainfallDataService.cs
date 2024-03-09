using Rainfall.Data;
using Rainfall.Data.Interfaces;

namespace Rainfall.Services.Interface
{
    public interface IRainfallDataService
    {
        Task<RainfallReadingResponse> GetRainfallDataAsync(string stationId);
    }
}