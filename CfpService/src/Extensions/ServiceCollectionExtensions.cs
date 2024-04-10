using CfpService.Infrastructure;
using CfpService.Mappers.ApplicationMapper;
using CfpService.Repositories.Activity;
using CfpService.Repositories.Application;
using CfpService.Services.Activity;
using CfpService.Services.Application;
using CfpService.Settings;

namespace CfpService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        AddRepositories(services);
        return services;
    }

    public static IServiceCollection AddModelsServices(this IServiceCollection services)
    {
        AddServices(services);
        return services;
    }
    
    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IApplicationMapper, ApplicationMapper>();
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IActivityService, ActivityService>();
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
    }

    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services, IConfigurationRoot config)
    {
        services.Configure<DbSettings>(config.GetSection(nameof(DbSettings)));
        Postgres.MapSettings();
        Postgres.AddMigrations(services);
        return services;
    }
}