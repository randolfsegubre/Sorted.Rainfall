using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rainfall.Data;
using Rainfall.Data.Interfaces;
using Rainfall.Services.Interface;
using System.Net.Http.Json;

namespace Rainfall.Services
{
    public class RainfallDataService : IRainfallDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _endpointUrl;

        public RainfallDataService(IConfiguration configuration)
        {
            // Add required services
            var services = new ServiceCollection();
            services.AddAuthorizationCore(); // Add authorization services

            // Create and configure HttpClient instance
            _httpClient = new HttpClient();
            _baseUrl = configuration.GetSection("AppSettings:BaseUrl").Value ?? string.Empty;
            _endpointUrl = configuration.GetSection("AppSettings:EndPointUrl").Value ?? string.Empty;
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<RainfallReadingResponse> GetRainfallDataAsync(string stationId, int count)
        {
            // Construct the API endpoint URL
            string formattedUri = _endpointUrl
                    .Replace("{root}", _baseUrl)
                    .Replace("{stationId}", stationId)
                    .Replace("{count}", count.ToString()) ;

            // Make GET request to the external API
            HttpResponseMessage response = await _httpClient.GetAsync(formattedUri);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response into appropriate data contract classes
                RainfallReadingResponse? rainfallData = await response.Content.ReadFromJsonAsync<RainfallReadingResponse>();
                return rainfallData ?? throw new Exception();
            }
            else
            {
                // Handle error response (e.g., log error, return null)
                return null;
            }
        }
    }
}
