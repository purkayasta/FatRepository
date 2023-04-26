using FatRepository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FatRepository.Test.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFatRepository<WeatherForecast, WeatherDbContext> _repository;
        private readonly IFatDatabase<WeatherDbContext> _database;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFatRepository<WeatherForecast, WeatherDbContext> forcastRepository, IFatDatabase<WeatherDbContext> fatUnitOfWork)
        {
            _logger = logger;
            _repository = forcastRepository;
            _database = fatUnitOfWork;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var vals = await _repository.AllAsync();
            return Ok(vals);
        }

        [HttpPost]
        public async Task<IActionResult> Post(WeatherForecast forcast)
        {
            await _repository.InsertOneAsync(forcast);
            await _database.CommitAsync();
            return Ok(forcast);
        }
    }
}