using FatRepository.Installer;
using FatRepository.SQLServer.Test.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<WeatherDbContext>(options =>
{
    options.UseInMemoryDatabase("weather-db");
    //options.UseSqlite("weather-db");
});

builder.Services.AddFatRepository();
//builder.Services.AddScoped(typeof(IWeatherRepo<,>), typeof(WeatherRepo<,>));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
