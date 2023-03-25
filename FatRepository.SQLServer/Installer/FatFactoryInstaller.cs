using FatRepository.SQLServer.Contracts;
using FatRepository.SQLServer.Implementions;
using Microsoft.EntityFrameworkCore;

namespace FatRepository.SQLServer.Installer
{
    public static class FatFactoryInstaller
    {
        public static IFatUnitOfWork CreateUnitOfWork(DbContext dbContext) => new FatUnitOfWork(dbContext);
        public static IFatRepository<T> CreateFatRepository<T>(DbContext dbContext) where T : class => new FatRepository<T>(dbContext);

    }
}
