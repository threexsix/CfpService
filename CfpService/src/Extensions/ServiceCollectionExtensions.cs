using CfpService.Settings;

namespace CfpService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        AddRepositories(services);
        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        // services.AddScoped<IApplicationRepository, ApplicationRepository>();
    }

    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services, IConfigurationRoot config)
    {
        services.Configure<DbSettings>(config.GetSection(nameof(DbSettings)));
        Postgres.AddMigrations(services);
        return services;
    }
}