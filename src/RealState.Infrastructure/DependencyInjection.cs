using Microsoft.Extensions.DependencyInjection;
using RealState.Application.Common.Interfaces.Authentication;
using RealState.Application.Common.Interfaces.Persistence;
using RealState.Infrastructure.Authentication;
using RealState.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using RealState.Infrastructure.Persistence.Repositories;
using RealState.Infrastructure.Common;
using RealState.Infrastructure.Persistence.Interceptors;

namespace RealState.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuth(configuration);
            services.AddPersistence();
            return services;
        
        }


        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            var dbSettings = new DbSettings();
            services.AddDbContext<RealStateDbContext>(options =>
                options.UseSqlServer("Server=db;Database=RealStateDB;User=sa;Password=realState123;Encrypt=Falsee")
            );

          
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyBuildingRepository, PropertyBuildingRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<PublishDomainEventsInterceptor>();

            return services;

        }


        private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

            return services;
        }
    }
}