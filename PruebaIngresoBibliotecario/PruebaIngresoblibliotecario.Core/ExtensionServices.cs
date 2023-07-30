using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PruebaIngresoblibliotecario.Core.Interfaces.Services;
using PruebaIngresoblibliotecario.Core.Mappers;
using PruebaIngresoblibliotecario.Core.Services;

namespace PruebaIngresoblibliotecario.Core
{
    public static class ExtensionServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddMappingCore();
            services.AddScoped<IPrestamoService, PrestamoService>();
            return services;
        }

        public static void AddMappingCore(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(confi =>
            {
                confi.AddProfile(new PrestamoMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}