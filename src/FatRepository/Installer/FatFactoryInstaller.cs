using FatRepository.Contracts;
using FatRepository.Implementions;
using Microsoft.EntityFrameworkCore;

namespace FatRepository.Installer
{
    public static class FatFactoryInstaller
    {
        public static IFatUnitOfWork<TDbContext> CreateUnitOfWork<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
            => new FatUnitOfWork<TDbContext>(dbContext);

        public static IFatRepository<TEntity, TDbContext> CreateFatRepository<TEntity, TDbContext>(TDbContext dbContext) where TEntity : class where TDbContext : DbContext
            => new FatRepository<TEntity, TDbContext>(dbContext);

    }
}
