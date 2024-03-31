using FluentMigrator.Runner;

namespace CfpService.Extensions;

public static class HostExtensions
{
    public static IHost MigrateUp(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
        return app;
    }
    public static IHost MigrateDown(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(0);
        return app;
    }
}