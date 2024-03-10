using Microsoft.Extensions.Logging;
using Moq;
using Rainfall.Data;
using Rainfall.Services.Interface;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Rainfall.Test.Controllers
{
    public class RainfallControllerTests
    {
        private readonly Mock<ILogger<RainfallController>> _loggerMock;
        private readonly Mock<IRainfallDataService> _rainfallDataServiceMock;
        private readonly RainfallController _controller;

        public RainfallControllerTests()
        {
            _loggerMock = new Mock<ILogger<RainfallController>>();
            _rainfallDataServiceMock = new Mock<IRainfallDataService>();
            _controller = new RainfallController(_loggerMock.Object, _rainfallDataServiceMock.Object);
        }

        [Theory]
        [InlineData("station123")]
        public async Task GetRainfallReadings_HappyPath_CorrectStationId_Returns_OkResult(string stationId)
        {
            // Arrange
            var expectedResponse = new RainfallResponse
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = new Root() // Mocked data for happy path
            };
            _rainfallDataServiceMock.Setup(x => x.GetRainfallDataAsync(stationId))
                                   .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetRainfallReadings(stationId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetRainfallReadings_UnhappyPath_NoStationId_Returns_BadRequest(string stationId)
        {
            // Act
            var result = await _controller.GetRainfallReadings(stationId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Theory]
        [InlineData("invalidStationId")]
        public async Task GetRainfallReadings_UnhappyPath_WrongStationId_Returns_NotFoundResult(string stationId)
        {
            // Arrange
            var expectedResponse = new RainfallResponse
            {
                Success = false,
                StatusCode = HttpStatusCode.NotFound,
                Error = new ErrorResponse { Message = "Station not found" }
            };
            _rainfallDataServiceMock.Setup(x => x.GetRainfallDataAsync(stationId))
                                   .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetRainfallReadings(stationId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
            Assert.Equal(expectedResponse.Error, notFoundResult.Value);
        }
    }
}
