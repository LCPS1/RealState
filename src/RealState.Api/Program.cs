using Microsoft.AspNetCore.Mvc.Infrastructure;
using RealState.Api;
using RealState.Api.Common.Errors;
using RealState.Application.Services;
using RealState.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPresentation();
}



var app = builder.Build();
{

    app.UseExceptionHandler("/error");

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}

