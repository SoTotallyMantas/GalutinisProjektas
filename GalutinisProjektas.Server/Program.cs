using GalutinisProjektas.Server.Models;
using GalutinisProjektas.Server.Service;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);




// Configure HttpClient
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
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
