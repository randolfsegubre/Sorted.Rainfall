namespace Rainfall.Test.Helper
{
    /// <summary>
    /// Test Helper methods
    /// </summary>
    public class TestHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            config.GetSection("AppSettings").Get<AppSettings>();
            return config;
        }
    }
}
