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
            ('report', 'Доклад, 35-45 минут'),
            ('masterclass', 'Мастеркласс, 1-2 часа'),
            ('discussion', 'Дискуссия / круглый стол, 40-50 минут');
        ";

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
            delete from activity_types 
            where name in ('report', 'masterclass', 'discussion');
        ";

        Execute.Sql(sql);
    }
}