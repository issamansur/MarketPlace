using System.Text.Json;
using System.Text.Json.Serialization;
using MarketPlace.Application.Common;
using MarketPlace.Application.DI;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services from other layers to the container
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddContracts();
    
    // TODO: How to configure options in layer by DI
    builder.Services.Configure<ProjectSettings>(
        builder.Configuration.GetSection("ProjectSettings"));
}

// Configure connection to the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MarketPlaceDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // For the snake_case
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
        // Enum as string, not as number
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Add CORS policy for production
    // TODO: configure for production
    app.UseCors();
}

app.UseHttpsRedirection();

// Before mapping, I think...
// TODO: Refactor static folder in NON-PROJECT folder
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "UserAdvertisements")
            ),
        RequestPath = "/UserAdvertisements"
    }
);

// Disable for the test
//app.UseAuthorization();

app.MapControllers();

app.UseStatusCodePages();

app.Run();