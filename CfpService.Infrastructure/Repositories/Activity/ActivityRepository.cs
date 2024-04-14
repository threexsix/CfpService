using CfpService.Application.Entities;
using CfpService.Application.Repositories.Activity;
using CfpService.Infrastructure.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CfpService.Infrastructure.Repositories.Activity;

public class ActivityRepository : IActivityRepository
{
    private readonly string _connectionString;

    public ActivityRepository(IOptions<DbSettings> options)
    {
        _connectionString = options.Value.PostgresConnectionString;
    }

    public async Task<IEnumerable<ApplicationActivity>> GetAllActivities()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select  *
                from    activity_types
                ";
        
        var activities = await connection.QueryAsync<ApplicationActivity>(sql);
        return activities.ToList();
    }
}