using FatRepository.Contracts;
using FatRepository.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace FatRepository.Installer
{
	public static class MSServiceContainer
    {
        /// <summary>
        /// Registering and Resolving FatInterfaces with Implementation Classes
        /// </summary>
        /// <param name="services"></param>
        public static void AddFatRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IFatRepository<,>), typeof(FatRepository<,>));
            services.AddScoped(typeof(IFatDatabase<>), typeof(FatDatabase<>));
        }
    }
}
