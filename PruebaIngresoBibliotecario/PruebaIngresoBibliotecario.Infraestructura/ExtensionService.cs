using Microsoft.Extensions.DependencyInjection;
using PruebaIngresoBibliotecario.Infraestructura.Repositories;
using PruebaIngresoblibliotecario.Core.Interfaces.Repository;

namespace PruebaIngresoBibliotecario.Infraestructura
{
    public static class ExtensionService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPrestamoRepository, Prestamorepository>();
            return services;
        }
    }
}