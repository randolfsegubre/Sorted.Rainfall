using Rainfall.Data.Interfaces;
using System.Net.Http.Json;
using System.Text;

namespace Rainfall.Test.RainfallServicesTest
{
    public class RainfallDataServiceTests
    {
        private readonly Mock<IHttpClientWrapper> _mockHttpClientWrapper;
        private readonly Mock<ILogger<RainfallDataService>> _mockLogger;
        private readonly IConfiguration _configuration;

        public RainfallDataServiceTests()
        {
            _mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            _mockLogger = new Mock<ILogger<RainfallDataService>>();
            _configuration = TestHelper.InitConfiguration();
        }

        [Fact]
        public async Task GetRainfallDataAsync_HappyPath_CorrectStationId()
        {
            // Arrange
            var expectedContent = new StringContent(Resource.SortedRainfallApi_Response, Encoding.UTF8, "application/json"); // Set expected content.
            var expectedData = await expectedContent.ReadFromJsonAsync<Root>(); // Set epected Data

            // Create HttpResponseMessage with OK status code and content from JSON string
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(Resource.SortedRainfallApi_Response, Encoding.UTF8, "application/json")
            };

            var response = new RainfallResponse
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Data = expectedData
            };
    
            _mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResponse);

            var rainfallService = new RainfallDataService(_mockLogger.Object, _mockHttpClientWrapper.Object);

            // Act
            var result = await rainfallService.GetRainfallDataAsync(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expectedData?.Context, result.Data.Context);
            Assert.True(result.Data.items.Any());
        }

        [Theory]
        [InlineData("wrongStationId")]
        [InlineData("")]
        public async Task GetRainfallDataAsync_UnhappyPath_WrongOrNoStationId(string stationId)
        {
            // Arrange
            _mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ThrowsAsync(new HttpRequestException()); // Simulate exception

            var rainfallService = new RainfallDataService(_mockLogger.Object, _mockHttpClientWrapper.Object    );

            // Act
            var result = await rainfallService.GetRainfallDataAsync(stationId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            // Additional assertions for error details can be added if needed
        }

        [Fact]
        public async Task GetRainfallDataAsync_ExceptionThrown()
        {
            // Arrange
            var expectedErrorMessage = "expected exception message";
            _mockHttpClientWrapper.Setup(x => x.GetAsync(It.IsAny<string>()))
                                  .ThrowsAsync(new Exception(expectedErrorMessage));

            var rainfallDataService = new RainfallDataService(_mockLogger.Object, _mockHttpClientWrapper.Object);

            // Act
            var response = await rainfallDataService.GetRainfallDataAsync(It.IsAny<string>());

            // Assert
            Assert.False(response.Success);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal(expectedErrorMessage, response.Error.Message);
        }
    }
}

