using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Rainfall.Data;
using Rainfall.Services.Interface;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Rainfall.Test.Controllers
{
    public class RainfallControllerTests
    {
        #region Fields
        private readonly Mock<ILogger<RainfallController>> _loggerMock;
        private readonly Mock<IRainfallDataService> _rainfallDataServiceMock;
        private readonly RainfallController _controller;
        #endregion Fields

        #region Constructors
        public RainfallControllerTests()
        {
            _loggerMock = new Mock<ILogger<RainfallController>>();
            _rainfallDataServiceMock = new Mock<IRainfallDataService>();
            _controller = new RainfallController(_loggerMock.Object, _rainfallDataServiceMock.Object);
        }
        #endregion Constructors

        #region Happy Path Get
        [Theory]
        [InlineData("station123")]
        public async Task GetRainfallReadings_HappyPath_CorrectStationId_Returns_OkResult(string stationId)
        {
            // Arrange
            var expectedResponse = new RainfallResponse
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = JsonConvert.DeserializeObject<Root>(Resource.SortedRainfallApi_Response) // Mocked data for happy path
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

        #endregion Happy Path Get

        #region Unhappy Path Get
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

        #endregion Unhappy Path Get

        #region Happy Path Post
        [Theory]
        [InlineData("station123")]
        public async Task PostRainfallDataAsync_HappyPath_CorrectStationId_Returns_OkResult(string stationId)
        {
            // Arrange
            var expectedResponse = new RainfallResponse
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = JsonConvert.DeserializeObject<Root>(Resource.SortedRainfallApi_Response) // Mocked data for happy path
            };
            _rainfallDataServiceMock.Setup(x => x.GetRainfallDataAsync(stationId))
                                   .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.PostRainfallDataAsync(stationId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }
        #endregion Happy Path Post

        #region Unhappy Path Post
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task PostRainfallDataAsync_UnhappyPath_NoStationId_Returns_BadRequest(string stationId)
        {
            // Act
            var result = await _controller.PostRainfallDataAsync(stationId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Theory]
        [InlineData("invalidStationId")]
        public async Task PostRainfallDataAsync_UnhappyPath_WrongStationId_Returns_NotFoundResult(string stationId)
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
            var result = await _controller.PostRainfallDataAsync(stationId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
            Assert.Equal(expectedResponse.Error, notFoundResult.Value);
        }

        [Fact]
        public async Task PostRainfallDataAsync_UnhappyPath_ExceptionThrown_Returns_InternalServerError()
        {
            // Arrange
            var stationId = "station123";
            var expectedErrorMessage = "expected exception message";
            _rainfallDataServiceMock.Setup(x => x.GetRainfallDataAsync(stationId))
                                    .ThrowsAsync(new Exception(expectedErrorMessage));

            // Act
            var result = await _controller.PostRainfallDataAsync(stationId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
            Assert.Equal("An unexpected error occurred", objectResult.Value);
        }

        #endregion Unhappy Path Post
    }
}
