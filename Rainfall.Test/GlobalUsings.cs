global using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Rainfall.Services.Config;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();