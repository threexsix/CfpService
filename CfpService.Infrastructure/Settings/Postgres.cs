using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CfpService.Infrastructure.Settings;

public static class Postgres
{
    public static void MapSettings()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true; ;
    }

    public static void AddMigrations(IServiceCollection services)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                {
                    var cfg = s.GetRequiredService<IOptions<DbSettings>>();
                    return cfg.Value.PostgresConnectionString;
                })
                .ScanIn(typeof(Postgres).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}