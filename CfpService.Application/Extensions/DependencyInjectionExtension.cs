using System.Reflection;
using CfpService.Application.Mappers.ActivityMapper;
using CfpService.Application.Mappers.ApplicationMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CfpService.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IApplicationMapper, ApplicationMapper>();
        services.AddScoped<IActivityMapper, ActivityMapper>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionExtension).Assembly));
        
        return services;
    }
}