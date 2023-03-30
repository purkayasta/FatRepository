using FatRepository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FatRepository.Test.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFatRepository<WeatherForecast, WeatherDbContext> _forcastRepository;
        private readonly IFatUnitOfWork<WeatherDbContext> _fatUnitOfWork;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFatRepository<WeatherForecast, WeatherDbContext> forcastRepository, IFatUnitOfWork<WeatherDbContext> fatUnitOfWork)
        {
            _logger = logger;
            _forcastRepository = forcastRepository;
            _fatUnitOfWork = fatUnitOfWork;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var vals = await _forcastRepository.AllAsync();
            return Ok(vals);
        }

        [HttpPost]
        public async Task<IActionResult> Post(WeatherForecast forcast)
        {
            await _forcastRepository.InsertOneAsync(forcast);
            await _fatUnitOfWork.CommitAsync();
            return Ok(forcast);
        }
    }
}