﻿using Microsoft.AspNetCore.Mvc;
using Rainfall.Data.Interface;
using Rainfall.Services.Interface;
using System.Net;

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


    /// <summary>
    /// GET: /api/rainfall/{stationId}/readings
    /// Retrieves rainfall readings for the specified stationId
    /// </summary>
    /// <param name="stationId"></param>
    /// <returns></returns>
    [HttpGet("{stationId}", Name = "GetRainfallReadings")]
    public async Task<IActionResult> GetRainfallReadings(string stationId)
    {
        if (string.IsNullOrWhiteSpace(stationId))
        {
            return BadRequest("Station ID is required");
        }

        try
        {
            // Call the service method to get rainfall data
            IRainfallResponse response = await _rainfallDataService.GetRainfallDataAsync(stationId);

            // Check if the operation was successful
            if (response.Success)
            {
                // Check the status code and return the corresponding action
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK: // 200
                        return Ok(response);
                    case HttpStatusCode.NoContent: // 204
                        return Accepted(response);
                    default:
                        return StatusCode((int)response.StatusCode, response);
                }
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(response.Error);
                }
                else
                {
                    // Handle other error responses
                    return StatusCode((int)response.StatusCode, response.Error);
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An unexpected error occurred");

            // Return 500 status code response
            return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");
        }
    }

    /// <summary>
    /// Retrieves rainfall data asynchronously based on the provided station ID.
    /// </summary>
    /// <param name="stationId">The station ID for which rainfall data is requested.</param>
    /// <returns>An IActionResult representing the HTTP response.</returns>
    [HttpPost]
    public async Task<IActionResult> PostRainfallDataAsync([FromBody] string stationId)
    {
        if (string.IsNullOrWhiteSpace(stationId))
        {
            return BadRequest("Station ID is required");
        }

        try
        {
            // Call the service method to get rainfall data
            IRainfallResponse response = await _rainfallDataService.GetRainfallDataAsync(stationId);

            // Check if the operation was successful
            if (response.Success)
            {
                // Check the status code and return the corresponding action
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK: // 200
                        return Ok(response);
                    case HttpStatusCode.Accepted: // 202
                        return Accepted(response);
                    default:
                        return StatusCode((int)response.StatusCode, response);
                }
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(response.Error);
                }
                else
                {
                    // Handle other error responses
                    return StatusCode((int)response.StatusCode, response.Error);
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An unexpected error occurred");

            // Return 500 status code response
            return StatusCode((int)HttpStatusCode.InternalServerError, "An unexpected error occurred");
        }
    }
}
