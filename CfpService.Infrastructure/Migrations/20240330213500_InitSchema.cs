using FluentMigrator;

namespace CfpService.Infrastructure.Migrations;

[Migration(20240330213500)]
public class InitSchema : Migration
{
    public override void Up()
    {
        const string sql = @"
		create table activity_types 
		(
			name varchar primary key unique,
			description varchar
		);

		create table applications
		(
			id uuid primary key default gen_random_uuid() not null,
			author uuid not null,
			activity varchar references activity_types(name),
			name varchar(100),
			description varchar(300),
			outline varchar(1000),
			created_at timestamp default now(),
			submitted_at timestamp
		);
        ";
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
		drop table applications;
		drop table activity_types;
		";
	    
        Execute.Sql(sql);
    }
}