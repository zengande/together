using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class EditActivityDateTimeColunms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityTime",
                table: "activities",
                newName: "StartTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 30, 13, 48, 30, 903, DateTimeKind.Local));

            migrationBuilder.AddColumn<DateTime>(
                name: "ActivitDate",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndRegisterDate",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivitDate",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "EndRegisterDate",
                table: "activities");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "activities",
                newName: "ActivityTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 30, 13, 48, 30, 903, DateTimeKind.Local),
                oldClrType: typeof(DateTime));
        }
    }
}
