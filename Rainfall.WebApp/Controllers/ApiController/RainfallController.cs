using Microsoft.AspNetCore.Mvc;
using Rainfall.Data;
using Rainfall.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class RainfallController : ControllerBase
{
    private readonly ILogger<RainfallController> _logger;
    private readonly IRainfallDataService _rainfallDataService;

    public RainfallController(ILogger<RainfallController> logger, IRainfallDataService rainfallDataService)
    {
        _logger = logger;
        _rainfallDataService = rainfallDataService;
    }

    // GET: /api/rainfall/{stationId}/readings
    // Retrieves rainfall readings for the specified stationId
    [HttpGet("{stationId}", Name = "GetRainfallReadings")]
    public async Task<IActionResult> GetRainfallReadings(string stationId)
    {
        try
        {
            // Call service to fetch rainfall data
            var rainfallData = await _rainfallDataService.GetRainfallDataAsync(stationId);

            // Return 200 OK response with rainfall data
            return Ok(rainfallData); // Assuming rainfallData is a collection of rainfall readings
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred while processing the request.");

            // Return 500 Internal Server Error response
            return StatusCode(500, new ErrorResponse
            {
                Message = "Internal server error",
                Details = new List<ErrorDetail>()
            });
        }
    }

    // Add other controller actions as needed...
}
