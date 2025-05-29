using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindTrack.Models.Migrations
{
    /// <inheritdoc />
    public partial class fixTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategories_UserTasks_UserTaskTask_id",
                table: "TaskCategories");

            migrationBuilder.DropIndex(
                name: "IX_TaskCategories_UserTaskTask_id",
                table: "TaskCategories");

            migrationBuilder.DropColumn(
                name: "UserTaskTask_id",
                table: "TaskCategories");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_Category_id",
                table: "UserTasks",
                column: "Category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_TaskCategories_Category_id",
                table: "UserTasks",
                column: "Category_id",
                principalTable: "TaskCategories",
                principalColumn: "Category_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_TaskCategories_Category_id",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTasks_Category_id",
                table: "UserTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "UserTaskTask_id",
                table: "TaskCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskCategories_UserTaskTask_id",
                table: "TaskCategories",
                column: "UserTaskTask_id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategories_UserTasks_UserTaskTask_id",
                table: "TaskCategories",
                column: "UserTaskTask_id",
                principalTable: "UserTasks",
                principalColumn: "Task_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
