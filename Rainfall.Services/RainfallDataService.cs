using Microsoft.Extensions.Logging;
using Rainfall.Data;
using Rainfall.Services.Config;
using Rainfall.Services.Interface;
using System.Net;
using System.Net.Http.Json;

namespace Rainfall.Services
{
    public class RainfallDataService : IRainfallDataService
    {
        #region Declartions
        private readonly ILogger<RainfallDataService> _logger;
        private readonly HttpClient _httpClient;
        #endregion Declartions

        public RainfallDataService(ILogger<RainfallDataService> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }


        /// <summary>
        /// Method to get rainfall from external api
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<RainfallReadingResponse> GetRainfallDataAsync(string stationId)
        {
            try
            {
                // Construct the API endpoint URL
                string formattedUri = AppSettings.EndPointUrl
                        .Replace(Constants.root, AppSettings.BaseUrl)
                        .Replace(Constants.stationId, stationId);

                // Make GET request to the external API
                HttpResponseMessage response = await _httpClient.GetAsync(formattedUri);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the JSON response into appropriate data contract classes
                    RainfallReadingResponse? rainfallData = await response.Content.ReadFromJsonAsync<RainfallReadingResponse>();

                    return rainfallData;
                }
                else
                {
                    // Handle error response (e.g., log error, return null)
                    _logger.LogWarning(message: Constants.RainfallExternalApiResponseNotOk);

                    // intended throw new web exception
                    throw new WebException($"{HttpStatusCode.InternalServerError} - Internal Server Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(message: Constants.RainfallServiceErrorMsg, args: ex);
                throw;
            }
        }
    }
}
