using GalutinisProjektas.Server.Entity;
using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Adding Memory Cache
builder.Services.AddMemoryCache();
// Configure HttpClient
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddDbContextPool<ModeldbContext>(options =>
    options.UseMySQL(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"))
 .EnableSensitiveDataLogging()  // Enable to log parameter values
               .LogTo(Console.WriteLine, LogLevel.Information));  // Log SQL statements to console


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
  {
    Title = "GalutinisProjektas",
    Version = "v1",
    Description = "ASP.NET Core Web API for GalutinisProjektas",
    Contact = new Microsoft.OpenApi.Models.OpenApiContact
    {
        Name = "Mantas Trojanovskis",
    }
  });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}
);
// Add OpenWeatherMap service
builder.Services.AddSingleton<OpenWeatherMapService>();
// Add CarbonInterface service
builder.Services.AddSingleton<CarbonInterfaceService>();
// Add CountryCodes service
builder.Services.AddScoped<CountryCodesService>();
// Add IATACodes service
builder.Services.AddScoped<IATACodesService>();
// Add FuelTypes service
builder.Services.AddScoped<FuelTypesService>();
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
