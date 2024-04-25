using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager_02.Data.Migrations;

/// <inheritdoc />
public partial class FirstMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                RowId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK__Users__6965AB5722265939", x => x.RowId);
            });

        migrationBuilder.CreateTable(
            name: "Projects",
            columns: table => new
            {
                RowId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProjectName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                TechnicalName = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                CreatedBy = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK__Projects__FFEE7431FFE565E3", x => x.RowId);
                table.ForeignKey(
                    name: "FK__Projects__Create__3E52440B",
                    column: x => x.CreatedBy,
                    principalTable: "Users",
                    principalColumn: "RowId");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Projects_CreatedBy",
            table: "Projects",
            column: "CreatedBy");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Projects");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
