using CfpService.Repositories;
using CfpService.Services;
using CfpService.Settings;

namespace CfpService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        AddRepositories(services);
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        AddServices(services);
        return services;
    }
    
    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IApplicationService, ApplicationService>();
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
    }

    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services, IConfigurationRoot config)
    {
        services.Configure<DbSettings>(config.GetSection(nameof(DbSettings)));
        Postgres.AddMigrations(services);
        return services;
    }
}