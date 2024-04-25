using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager_02.Data.Migrations;

/// <inheritdoc />
public partial class AddDateTimeToProjects : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedOn",
            table: "Projects",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldDefaultValueSql: "GETDATE()");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "CreatedOn",
            table: "Projects",
            type: "nvarchar(max)",
            nullable: false,
            defaultValueSql: "GETDATE()",
            oldClrType: typeof(DateTime),
            oldType: "datetime2");
    }
}
