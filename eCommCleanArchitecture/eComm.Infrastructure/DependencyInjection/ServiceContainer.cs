using eComm.Domain.Entities;
using eComm.Domain.Interfaces;
using eComm.Infrastructure.Data;
using eComm.Infrastructure.Repositories;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "DefaultConnect";
            services.AddDbContext<eCommDbcontext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(connectionString),
            sqlOption =>
            {
                sqlOption.EnableRetryOnFailure();
                sqlOption.MigrationsAssembly(typeof(eCommDbcontext).Assembly.FullName);
            })
            .UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>, GenericRepostitory<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepostitory<Category>>();

            return services;
        }
    }
}
