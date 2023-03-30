using Microsoft.EntityFrameworkCore;

namespace FatRepository.Test.API
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<WeatherForecast>? WeatherForecast { get; set; }
    }
}
