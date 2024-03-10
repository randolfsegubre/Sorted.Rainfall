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

        [Fact]
        public async Task GetRainfallData_HappyPath_CorrectStationId_Returns_OkResult()
        {
            // Arrange
            var expectedStationId = "station123";
            var expectedResponse = new RainfallResponse
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = new Root() // Mocked data for happy path
            };
            _rainfallDataServiceMock.Setup(x => x.GetRainfallDataAsync(expectedStationId))
                                   .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetRainfallData(expectedStationId);

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
        public async Task GetRainfallData_UnhappyPath_NoStationId_Returns_BadRequest(string stationId)
        {
            // Act
            var result = await _controller.GetRainfallData(stationId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetRainfallData_UnhappyPath_WrongStationId_Returns_NotFoundResult()
        {
            // Arrange
            var wrongStationId = "invalidStationId";
            var expectedResponse = new RainfallResponse
            {
                Success = false,
                StatusCode = HttpStatusCode.NotFound,
                Error = new ErrorResponse { Message = "Station not found" }
            };
            _rainfallDataServiceMock.Setup(x => x.GetRainfallDataAsync(wrongStationId))
                                   .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetRainfallData(wrongStationId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
            Assert.Equal(expectedResponse.Error, notFoundResult.Value);
        }
    }
}
