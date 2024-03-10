using Microsoft.Extensions.Logging;
using Rainfall.Data;
using Rainfall.Data.Interface;
using Rainfall.Services.Config;
using Rainfall.Services.Interface;
using System.Net;
using System.Net.Http.Json;

namespace Rainfall.Services
{
    public class RainfallDataService : IRainfallDataService
    {
        #region Declarations
        private readonly ILogger<RainfallDataService> _logger;
        private readonly IHttpClientWrapper _httpClientWapper;
        #endregion Declarations

        public RainfallDataService(ILogger<RainfallDataService> logger, IHttpClientWrapper httpClientWrapper)
        {
            _logger = logger;
            _httpClientWapper = httpClientWrapper;
        }

        public async Task<IRainfallResponse> GetRainfallDataAsync(string stationId)
        {
            try
            {
                // Construct the API endpoint URL
                string? formattedUri = AppSettings.EndPointUrl
                        .Replace(Constants.root, AppSettings.BaseUrl)
                        .Replace(Constants.stationId, stationId);

                // Make GET request to the external API
                HttpResponseMessage response = await _httpClientWapper.GetAsync(formattedUri);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the JSON response into appropriate data contract classes
                    Root? rainfallData = await response.Content.ReadFromJsonAsync<Root>();

                    // Check if rainfall data items are null or empty
                    if (rainfallData != null && (rainfallData.items == null || rainfallData.items.Count == 0))
                    {
                        return new RainfallResponse
                        {
                            Success = true,
                            StatusCode = HttpStatusCode.NoContent
                        };
                    }

                    // Construct and return success response
                    return new RainfallResponse
                    {
                        Success = true,
                        StatusCode = HttpStatusCode.OK,
                        Data = rainfallData
                    };
                }
                else
                {
                    // Handle error response
                    var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    return new RainfallResponse
                    {
                        Success = false,
                        StatusCode = response.StatusCode,
                        Error = errorResponse ?? new ErrorResponse { Message = "Unknown error" }
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while making the HTTP request");

                // If an HTTP request error occurs
                var response = new RainfallResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError, // Set the status code to InternalServerError
                    Error = new ErrorResponse // Populate the Error property with the error response
                    {
                        Message = "An error occurred while making the HTTP request"
                    }
                };

                // Return the response
                return response;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, ex.Message);

                // If an unexpected error occurs
                var response = new RainfallResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError, // Set the status code to InternalServerError
                    Error = new ErrorResponse // Populate the Error property with the error response
                    {
                        Message = ex.Message
                    }
                };

                // Return the response
                return response;
            }
        }
    }
}
