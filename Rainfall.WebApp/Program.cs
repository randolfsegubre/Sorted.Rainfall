using Rainfall.Services;
using Rainfall.WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRainfallDataService, RainfallDataService>();

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
