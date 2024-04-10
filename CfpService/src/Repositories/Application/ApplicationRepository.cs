using CfpService.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CfpService.Repositories.Application;

public class ApplicationRepository : IApplicationRepository
{
    private readonly string _connectionString;
    
    public ApplicationRepository(IOptions<DbSettings> options)
    {
        _connectionString = options.Value.PostgresConnectionString;
    }
    
    public Entities.Application GetById(Guid id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
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
            
        return connection.QuerySingleOrDefault<Entities.Application>(sql, new { Id = id });
    }

    public Entities.Application Add(Entities.Application application)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
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
        return connection.QueryFirstOrDefault<Entities.Application>(sql, application);
    }


    public Entities.Application Put(Entities.Application application)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
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
            Id = application.Id,
            Activity = application.Activity,
            Name = application.Name,
            Description = application.Description,
            Outline = application.Outline
        };
        return connection.QueryFirstOrDefault<Entities.Application>(sql, parameters);
    }

    public void Delete(Guid id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
            delete
            from    applications
            where   id = @Id
            and     submitted_at is null
        ";
        connection.Execute(sql, new { Id = id});
    }
    
    public void Submit(Guid id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
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
        
        connection.Execute(sql, parameters);
    }

    public IEnumerable<Entities.Application> GetSubmittedApplications(DateTime time)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
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

        return connection.Query<Entities.Application>(sql, new { Time = time });
    }
    
    public IEnumerable<Entities.Application> GetUnSubmittedApplications(DateTime time)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
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

        return connection.Query<Entities.Application>(sql, new { Time = time });
    }

    public Entities.Application GetUserUnSubmittedApplication(Guid userId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
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

        return connection.QuerySingleOrDefault<Entities.Application>(sql, new { Id = userId });
    }

    public bool ExistByApplicationId(Guid applicationId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select exists 
                (
                select 1
                
                from   applications
                
                where  id = @Id
                )
                ";

        return connection.QuerySingleOrDefault<bool>(sql, new { Id = applicationId });
    }
    
    public bool ExistUserDraft(Guid userId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select exists 
                    (
                    select 1
                    
                    from   applications
                    
                    where  author = @Id
                    and    submitted_at is null
                    )
                ";

        return connection.QuerySingleOrDefault<bool>(sql, new { Id = userId });
    }

    public bool IsSubmitted(Guid applicationId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select exists 
                    (
                    select 1
                    
                    from   applications
                    
                    where  id = @Id
                    and    submitted_at is not null
                    )
                ";

        return connection.QuerySingleOrDefault<bool>(sql, new { Id = applicationId });
    }
}