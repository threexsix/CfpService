using FluentMigrator;

namespace CfpService.Migrations;

[Migration(20240330215500)]
public class SeedData : Migration
{
    public override void Up()
    {
        const string sql = @"
            insert into activity_types (name, description) 
            values 
            ('Report', 'Доклад, 35-45 минут'),
            ('Masterclass', 'Мастеркласс, 1-2 часа'),
            ('Discussion', 'Дискуссия / круглый стол, 40-50 минут');
        ";

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
            delete from activity_types 
            where name in ('Report', 'Masterclass', 'Discussion');
        ";

        Execute.Sql(sql);
    }
}