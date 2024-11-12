using eComm.Application.Mapping;
using eComm.Application.Services.Implementations;
using eComm.Application.Services.Implementations.Authentication;
using eComm.Application.Services.Interfaces;
using eComm.Application.Services.Interfaces.Authentication;
using eComm.Application.Validation;
using eComm.Application.Validation.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
namespace eComm.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IValidationService, ValidationService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();
            services.AddValidatorsFromAssemblyContaining<LoginUserValidation>();

            services.AddScoped<IAuthencationService, AuthenticationService>();
            return services;
        }
    }
}
