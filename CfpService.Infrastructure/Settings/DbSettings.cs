namespace CfpService.Infrastructure.Settings;

public class DbSettings
{
    public required string PostgresConnectionString { get; init; } = string.Empty;
}