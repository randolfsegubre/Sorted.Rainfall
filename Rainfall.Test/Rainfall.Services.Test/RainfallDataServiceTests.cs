using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Rainfall.Data;
using Rainfall.Services;
using Rainfall.Services.Config;
using Rainfall.Services.Interface;
using Rainfall.Test.Config;
using System.Net;

namespace Rainfall.Test.Rainfall.Services.Test
{
    public class RainfallDataServiceTests
    {
        #region Declartions and Mocking

        IConfiguration config;
        IRainfallDataService _rainfallDataService;
        Mock<IHttpClientWrapper> _mockHttpClient = new Mock<IHttpClientWrapper>();
        Mock<ILogger<RainfallDataService>> mockLogger = new Mock<ILogger<RainfallDataService>>();

        #endregion Declartions

        #region Constructors

        public RainfallDataServiceTests()
        {
            _rainfallDataService = new RainfallDataService(mockLogger.Object);
            config = InitConfiguration();
        }

        #endregion Constructors

        /// <summary>
        /// Assuming this is a happy path
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="count"></param>
        [Theory]
        [InlineData("3680")]
        public async Task GetRainfallDataAsync_Valid_Success(string stationId)
        {
            config.GetSection("AppSettings").Get<AppSettings>();

            string formattedUri = AppSettings.EndPointUrl
                    .Replace(Constants.root, AppSettings.BaseUrl)
                    .Replace(Constants.stationId, stationId);

            var httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(Resource.SortedRainfallApi_Response),
            };


            _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>()))
                          .ReturnsAsync(httpResponseMessage);

            //act
            var act = await _rainfallDataService.GetRainfallDataAsync(stationId);

            //assert
            Assert.NotNull(act);
            Assert.NotNull(act.items);
            Assert.True(act.items.Any());
        }

        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}
