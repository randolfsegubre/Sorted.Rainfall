using Rainfall.Data;
using Rainfall.Data.Interface;
using Rainfall.Data.Interfaces;
using Rainfall.Services;
using Rainfall.Services.Config;
using Rainfall.Services.Helper;
using Rainfall.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRainfallDataService, RainfallDataService>();
builder.Services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
builder.Services.AddSingleton<IRainfallResponse, RainfallResponse>();
builder.Services.AddSingleton<IRoot, Root>();
builder.Services.AddSingleton<IItem, Item>();
builder.Services.AddSingleton<IMeta, Meta>();
builder.Services.AddSingleton<IErrorDetail, ErrorDetail>();
builder.Services.AddSingleton<IErrorResponse, ErrorResponse>();
builder.Services.AddHttpClient();

// Add SwaggerGen services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
