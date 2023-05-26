using FluentMigrator;

namespace backend.Data.Migrations;

[Migration(202326042001223)]
public class SecondMigration : Migration
{
    public override void Up()
    {

        Alter.Table("Sensor").AddColumn("RecordedAt").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime);

    }

    public override void Down()
    {
        Delete.Table("Sensor");
        Delete.Table("House");
    }
}