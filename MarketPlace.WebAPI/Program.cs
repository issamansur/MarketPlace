using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.S3;
using MarketPlace.Application.DI;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Options;
using MarketPlace.WebAPI.Middlewares;
using MarketPlace.WebAPI.Middlewares.ExceptionHandler;
using MarketPlace.WebAPI.Middlewares.S3FileProvider;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services from other layers to the container
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
}

// Error Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.UseMiddleware<ImageResizingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // TODO: configure for production (no host and port)
    app.UseCors();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

// Before mapping, I think...
var staticFilesOptions = app.Configuration.GetSection(nameof(StaticFilesOptions)).Get<StaticFilesOptions>()!;

var staticFilesRequestPath = staticFilesOptions.RequestPath;
var staticFilesRealPath = staticFilesOptions.RealPath;
var staticFilesRealPathFull = Path.Combine(
    Directory.GetCurrentDirectory(), 
    staticFilesRealPath.TrimStart('/')
);

if (!Directory.Exists(staticFilesRealPathFull))
{
    Directory.CreateDirectory(staticFilesRealPathFull);
}

var cacheExpireInMinutes = staticFilesOptions.CacheExpireInMinutes;
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new S3FileProvider(
            app.Services.GetRequiredService<IAmazonS3>(),
            app.Configuration.GetSection(nameof(AWSOptions)).Get<AWSOptions>()!.ImageBucketName
        ),
        RequestPath = staticFilesRequestPath,
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Cache-Control",
                $"public, max-age={cacheExpireInMinutes * 60}");
            ctx.Context.Response.Headers.Append("Expires",
                DateTime.UtcNow.AddMinutes(cacheExpireInMinutes).ToString("R"));
        }
    }
);

// Disable for the test
//app.UseAuthorization();

app.MapControllers();

app.UseStatusCodePages();

app.Run();