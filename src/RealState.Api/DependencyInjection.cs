using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RealState.Api.Common.Errors;
using RealState.Api.Common.Mapping;

namespace RealState.Api
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection AddPresentation (this IServiceCollection service)
        {
            service.AddMappings();
            service.AddSingleton<ProblemDetailsFactory,RealStateProblemsDetailFactory>();
            service.AddControllers();
            return service;
        }
        
    }
}