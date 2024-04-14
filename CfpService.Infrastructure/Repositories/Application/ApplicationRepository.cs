using CfpService.Application.Entities;
using CfpService.Application.Repositories.Application;
using CfpService.Infrastructure.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CfpService.Infrastructure.Repositories.Application;

public class ApplicationRepository : IApplicationRepository
{
    private readonly string _connectionString;
    
    public ApplicationRepository(IOptions<DbSettings> options)
    {
        _connectionString = options.Value.PostgresConnectionString;
    }
    
    public async Task<ConferenceApplication?> GetByIdAsync(Guid id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline,
                        created_at,
                        submitted_at
                from    applications
                where   id = @Id
                ";
            
        return await connection.QuerySingleOrDefaultAsync<ConferenceApplication>(sql, new { Id = id });
    }

    public async Task<ConferenceApplication?> AddAsync(ConferenceApplication application)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
            insert into applications
                    (
                    author,
                    activity,
                    name,
                    description,
                    outline,
                    created_at,
                    submitted_at
                    )
            values  (
                     @Author,
                     @Activity,
                     @Name,
                     @Description,
                     @Outline,
                     @CreatedAt,
                     @SubmittedAt
                    )
            returning id, author, activity, name, description, outline, created_at, submitted_at;
            ";
        return await connection.QueryFirstOrDefaultAsync<ConferenceApplication>(sql, application);
    }


    public async Task<ConferenceApplication?> PutAsync(ConferenceApplication application)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
            update applications
            
            set     activity = @Activity,
                    name = @Name,
                    description = @Description,
                    outline = @Outline
        
            where   id = @Id
            and     submitted_at is null
            
            returning id, author, activity, name, description, outline, created_at, submitted_at;
        ";
        var parameters = new
        {
            application.Id,
            application.Activity,
            application.Name,
            application.Description,
            application.Outline
        };
        return await connection.QueryFirstOrDefaultAsync<ConferenceApplication>(sql, parameters);
    }

    public async Task DeleteAsync(Guid id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
            delete
            from    applications
            where   id = @Id
            and     submitted_at is null
        ";
        await connection.ExecuteAsync(sql, new { Id = id});
    }
    
    public async Task SubmitAsync(Guid id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
            update applications
            set    submitted_at = @Time
            
            where  id = @Id
        ";
        var parameters = new
        {
            Id = id,
            Time = DateTime.UtcNow
        };
        
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<IEnumerable<ConferenceApplication>> GetSubmittedApplicationsAsync(DateTime time)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline,
                        created_at,
                        submitted_at
                
                from    applications
                
                where   submitted_at >= @Time
        ";

        var applications = await connection.QueryAsync<ConferenceApplication>(sql, new { Time = time });
        return applications.ToList();
    }
    
    public async Task<IEnumerable<ConferenceApplication>> GetUnSubmittedApplicationsAsync(DateTime time)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline,
                        created_at,
                        submitted_at
                
                from    applications
                
                where   created_at > @Time
                and     submitted_at is null
                ";

        var applications = await connection.QueryAsync<ConferenceApplication>(sql, new { Time = time });
        return applications.ToList();
    }

    public async Task<ConferenceApplication?> GetUserUnSubmittedApplicationAsync(Guid userId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline,
                        created_at,
                        submitted_at
                
                from    applications
                
                where   author = @Id
                and     submitted_at is null
                ";

        return await connection.QuerySingleOrDefaultAsync<ConferenceApplication>(sql, new { Id = userId });
    }

    public async Task<bool> ExistByApplicationIdAsync(Guid applicationId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select exists 
                (
                select 1
                
                from   applications
                
                where  id = @Id
                )
                ";

        return await connection.QuerySingleOrDefaultAsync<bool>(sql, new { Id = applicationId });
    }
    
    public async Task<bool> ExistUnsubmittedFromUserAsync(Guid userId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select exists 
                    (
                    select 1
                    
                    from   applications
                    
                    where  author = @Id
                    and    submitted_at is null
                    )
                ";

        return await connection.QuerySingleOrDefaultAsync<bool>(sql, new { Id = userId });
    }

    public async Task<bool> IsSubmittedAsync(Guid applicationId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        const string sql = @"
                select exists 
                    (
                    select 1
                    
                    from   applications
                    
                    where  id = @Id
                    and    submitted_at is not null
                    )
                ";

        return await connection.QuerySingleOrDefaultAsync<bool>(sql, new { Id = applicationId });
    }
}