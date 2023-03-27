using Microsoft.EntityFrameworkCore;

namespace FatRepository.SQLServer.Test.API
{
    public interface IWeatherRepo<T, TDbContext>
    {
        T? Get();
        List<T> GetAll();
    }

    public class WeatherRepo<T, TDbContext> : IWeatherRepo<T, TDbContext> where T : class where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public WeatherRepo(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T? Get() => _dbContext.Set<T>().FirstOrDefault();
        public List<T> GetAll() => _dbContext.Set<T>().ToList();
    }
}
