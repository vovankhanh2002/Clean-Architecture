using eComm.Application.Services.Interfaces;
using eComm.Domain.Entities;
using eComm.Domain.Entities.Identitys;
using eComm.Domain.Interfaces;
using eComm.Domain.Interfaces.Authentication;
using eComm.Infrastructure.Data;
using eComm.Infrastructure.Middleware;
using eComm.Infrastructure.Repositories;
using eComm.Infrastructure.Repositories.Authentication;
using eComm.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<eCommDbcontext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnect"),
            sqlOption =>
            {
                sqlOption.EnableRetryOnFailure();
                sqlOption.MigrationsAssembly(typeof(eCommDbcontext).Assembly.FullName);
            })
            .UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>, GenericRepostitory<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepostitory<Category>>();
            services.AddScoped(typeof(IAppLogger<>), (typeof(SeriLogLoggerAdapter<>)));

            services.AddDefaultIdentity<ApplicationUser>( option =>
            {
                option.SignIn.RequireConfirmedEmail = true;
                option.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                option.Password.RequireDigit = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.Password.RequiredUniqueChars = 1;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<eCommDbcontext>();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                };
            });
            services.AddScoped<IRoleManager, RoleManagerment>();
            services.AddScoped<IUerManager, UserManagerment>();
            services.AddScoped<ITokenManager, TokenManagerment>();
            return services;
        }
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();
            return app;
        }
    }
}
