using FatRepository.Contracts;
using FatRepository.Implementions;
using Microsoft.Extensions.DependencyInjection;

namespace FatRepository.Installer
{
    public static class MSServiceContainer
    {
        /// <summary>
        /// Registering and Resolving FatInterfaces with Implemention Classes
        /// </summary>
        /// <param name="services"></param>
        public static void AddFatRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IFatRepository<,>), typeof(FatRepository<,>));
            services.AddScoped(typeof(IFatUnitOfWork<>), typeof(FatUnitOfWork<>));
        }
    }
}
