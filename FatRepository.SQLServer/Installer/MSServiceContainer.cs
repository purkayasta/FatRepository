using FatRepository.SQLServer.Contracts;
using FatRepository.SQLServer.Implementions;
using Microsoft.Extensions.DependencyInjection;

namespace FatRepository.SQLServer.Installer
{
    public static class MSServiceContainer
    {
        public static void AddFatRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IFatRepository<>), typeof(FatRepository<>));
            services.AddScoped<IFatUnitOfWork, FatUnitOfWork>();
        }
    }
}
