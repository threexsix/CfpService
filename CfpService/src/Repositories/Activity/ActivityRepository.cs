using CfpService.Dtos.Activity;
using CfpService.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CfpService.Repositories.Activity;

public class ActivityRepository : IActivityRepository
{
    private readonly string _connectionString;

    public ActivityRepository(IOptions<DbSettings> options)
    {
        _connectionString = options.Value.PostgresConnectionString;
    }

    public IEnumerable<GetActivityDto> GetAllActivities()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select  *
                from    activity_types
                ";

        return connection.Query<GetActivityDto>(sql);
    }
}