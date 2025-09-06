using Application.services;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IMovimientoService, MovimientoService>();
            services.AddScoped<ITipoMovimientoService, TipoMovimientoService>();
            services.AddScoped<IProductosService, ProductosService>();

            return services;
        }
    }
}
