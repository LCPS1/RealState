using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealState.Application.Authentication.Commands.Register;
using ErrorOr;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using RealState.Application.Common.Behaviors;
using RealState.Application.Authentication.Common;


namespace RealState.Application.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Injections
            // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandSevice>();
            // services.AddScoped<IAuthenticationQueryService, AuthenticationQuerySevice>();

            //services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>, 
            
            services.AddScoped(typeof(IPipelineBehavior<,>),
                               typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            return services;
        }
    }
}