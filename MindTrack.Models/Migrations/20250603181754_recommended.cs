using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindTrack.Models.Migrations
{
    /// <inheritdoc />
    public partial class recommended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedTaskRecommended_Task_Id",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Recommended_Task_Id",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_RecommendedTaskRecommended_Task_Id",
                table: "UserTasks",
                column: "RecommendedTaskRecommended_Task_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_RecommendedTasks_RecommendedTaskRecommended_Task_Id",
                table: "UserTasks",
                column: "RecommendedTaskRecommended_Task_Id",
                principalTable: "RecommendedTasks",
                principalColumn: "Recommended_Task_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_RecommendedTasks_RecommendedTaskRecommended_Task_Id",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTasks_RecommendedTaskRecommended_Task_Id",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "RecommendedTaskRecommended_Task_Id",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "Recommended_Task_Id",
                table: "UserTasks");
        }
    }
}
