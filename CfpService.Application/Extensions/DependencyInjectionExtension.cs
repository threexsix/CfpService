using System.Reflection;
using CfpService.Application.Mappers.ActivityMapper;
using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Services.Activity;
using CfpService.Application.Services.Application;
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
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}