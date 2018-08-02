using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddCategoryIdCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivitDate",
                table: "activities");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "activities",
                newName: "EndRegisterTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "activities",
                newName: "ActivityStartTime");

            migrationBuilder.RenameColumn(
                name: "EndRegisterDate",
                table: "activities",
                newName: "ActivityEndTime");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "activities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "activities");

            migrationBuilder.RenameColumn(
                name: "EndRegisterTime",
                table: "activities",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "ActivityStartTime",
                table: "activities",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "ActivityEndTime",
                table: "activities",
                newName: "EndRegisterDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivitDate",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
