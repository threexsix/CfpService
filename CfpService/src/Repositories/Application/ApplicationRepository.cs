using CfpService.Dtos;
using CfpService.Dtos.Application;
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
    
    public GetApplicationDto GetById(Guid id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline
                from    applications
                where   id = @Id
                ";
            
        return connection.QuerySingleOrDefault<GetApplicationDto>(sql, new { Id = id });
    }

    public GetApplicationDto Add(PostApplicationDto dto)
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
                    outline
                    )
            values  (
                     @Author,
                     @Activity,
                     @Name,
                     @Description,
                     @Outline
                    )
            returning id, author, activity, name, description, outline;
            ";
        return connection.QueryFirstOrDefault<GetApplicationDto>(sql, dto);
    }


    public GetApplicationDto Put(Guid id, PutApplicationDto dto)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
        update applications
        set         activity = @Activity,
                    name = @Name,
                    description = @Description,
                    outline = @Outline
        
        where       id = @Id
        and         submitted_at is null
        returning id, author, activity, name, description, outline;
        ";
        var parameters = new
        {
            Id = id,
            Activity = dto.Activity,
            Name = dto.Name,
            Description = dto.Description,
            Outline = dto.Outline
        };
        return connection.QueryFirstOrDefault<GetApplicationDto>(sql, parameters);
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

    public IEnumerable<GetApplicationDto> GetSubmittedApplications(DateTime time)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline
                from    applications
                where   submitted_at >= @Time
        ";

        return connection.Query<GetApplicationDto>(sql, new { Time = time });
    }
    
    public IEnumerable<GetApplicationDto> GetUnSubmittedApplications(DateTime time)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline
                from    applications
                where   created_at > @Time
                and     submitted_at is null
                ";

        return connection.Query<GetApplicationDto>(sql, new { Time = time });
    }

    public GetApplicationDto GetUserUnSubmittedApplication(Guid userId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select  id,
                        author,
                        activity,
                        name,
                        description,
                        outline
                from    applications
                where   author = @Id
                and     submitted_at is null
                ";

        return connection.QuerySingleOrDefault<GetApplicationDto>(sql, new { Id = userId });
    }

    public bool Exist(Guid userId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select exists (
                        select 1
                        from applications
                        where author = @Id
                        and   submitted_at is null)
                ";

        return connection.QuerySingleOrDefault<bool>(sql, new { Id = userId });
    }

    public bool IsSubmitted(Guid applicationId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string sql = @"
                select exists (
                        select 1
                        from applications
                        where id = @Id
                        and   submitted_at is not null)
                ";

        return connection.QuerySingleOrDefault<bool>(sql, new { Id = applicationId });
    }
}