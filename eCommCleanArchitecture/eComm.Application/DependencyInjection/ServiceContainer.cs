using eComm.Application.Mapping;
using eComm.Application.Services.Implementations;
using eComm.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace eComm.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
