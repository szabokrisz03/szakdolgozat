using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager_02.Data.Migrations;

/// <inheritdoc />
public partial class ProjectTasks : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Tasks",
            columns: table => new
            {
                RowId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProjectId = table.Column<long>(type: "bigint", nullable: false),
                TaskName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                Priority = table.Column<int>(type: "int", nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("ProjectTaskRowId", x => x.RowId);
                table.ForeignKey(
                    name: "FK__Tasks__Create__0E2R3W2",
                    column: x => x.ProjectId,
                    principalTable: "Projects",
                    principalColumn: "RowId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Tasks_ProjectId",
            table: "Tasks",
            column: "ProjectId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Tasks");
    }
}
