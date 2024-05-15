using MarketPlace.Application.DI;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Contracts;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services from other layers to the container
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddContracts();
}

// Configure connection to the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MarketPlaceDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

// Disable for the test
//app.UseAuthorization();

app.MapControllers();

app.Run();