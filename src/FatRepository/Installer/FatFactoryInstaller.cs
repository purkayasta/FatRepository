using FatRepository.Contracts;
using FatRepository.Implementations;
using Microsoft.EntityFrameworkCore;

namespace FatRepository.Installer
{
	public static class FatFactoryInstaller
    {
        public static IFatDatabase<TDbContext> CreateUnitOfWork<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
            => new FatDatabase<TDbContext>(dbContext);

        public static IFatRepository<TEntity, TDbContext> CreateFatRepository<TEntity, TDbContext>(TDbContext dbContext) where TEntity : class where TDbContext : DbContext
            => new FatRepository<TEntity, TDbContext>(dbContext);

    }
}
