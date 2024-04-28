using CfpService.Application.Repositories.Activity;
using CfpService.Application.Repositories.Application;
using CfpService.Infrastructure.Repositories.Activity;
using CfpService.Infrastructure.Repositories.Application;
using CfpService.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CfpService.Infrastructure.Extension;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot config)
    {
        AddRepositories(services);
        AddDalInfrastructure(services, config); 
        return services;
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
    }

    private static void AddDalInfrastructure(this IServiceCollection services, IConfigurationRoot config)
    {
        services.Configure<DbSettings>(config.GetSection(nameof(DbSettings)));
        Postgres.MapSettings();
        Postgres.AddMigrations(services);
    }
}