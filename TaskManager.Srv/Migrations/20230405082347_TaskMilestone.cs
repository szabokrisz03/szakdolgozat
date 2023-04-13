using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Srv.Migrations
{
    /// <inheritdoc />
    public partial class TaskMilestone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "actual",
                table: "task_milestone",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "task_milestone",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "project_task_row_id",
                table: "task_milestone",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "connecting_wi_db",
                columns: table => new
                {
                    rowid = table.Column<long>(name: "row_id", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    wiid = table.Column<long>(name: "wi_id", type: "bigint", nullable: false),
                    taskid = table.Column<long>(name: "task_id", type: "bigint", nullable: false),
                    insertuser = table.Column<string>(name: "insert_user", type: "nvarchar(max)", nullable: true),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_connecting_wi_db", x => x.rowid);
                });

            migrationBuilder.CreateIndex(
                name: "ix_task_milestone_name",
                table: "task_milestone",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_task_milestone_project_task_row_id",
                table: "task_milestone",
                column: "project_task_row_id");

            migrationBuilder.CreateIndex(
                name: "ix_connecting_wi_db_task_id_wi_id",
                table: "connecting_wi_db",
                columns: new[] { "task_id", "wi_id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_task_milestone_project_task_project_task_row_id",
                table: "task_milestone",
                column: "project_task_row_id",
                principalTable: "project_task",
                principalColumn: "row_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_task_milestone_project_task_project_task_row_id",
                table: "task_milestone");

            migrationBuilder.DropTable(
                name: "connecting_wi_db");

            migrationBuilder.DropIndex(
                name: "ix_task_milestone_name",
                table: "task_milestone");

            migrationBuilder.DropIndex(
                name: "ix_task_milestone_project_task_row_id",
                table: "task_milestone");

            migrationBuilder.DropColumn(
                name: "name",
                table: "task_milestone");

            migrationBuilder.DropColumn(
                name: "project_task_row_id",
                table: "task_milestone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "actual",
                table: "task_milestone",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
