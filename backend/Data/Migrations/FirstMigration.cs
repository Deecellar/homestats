using FluentMigrator;

namespace backend.Data.Migrations;

[Migration(20231231235959)]
public class FirstMigration : Migration
{
    public override void Up()
    {

        Create.Table("House")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Address").AsString(50).NotNullable()
            .WithColumn("City").AsString(50).NotNullable()
            .WithColumn("State").AsString(50).NotNullable();

            // We have sensor types: Temperature, Humidity, SunExposure
            // We have value, recorded at, and house id
            // sensor type is a int, 0, 1, 2

        Create.Table("Sensor")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("Type").AsInt16().NotNullable()
            .WithColumn("Value").AsDouble().NotNullable()
            .WithColumn("HouseId").AsGuid().NotNullable()
            .ForeignKey("FK_Sensor_House", "House", "Id");

    }

    public override void Down()
    {
        Delete.Table("Sensor");
        Delete.Table("House");
    }
}