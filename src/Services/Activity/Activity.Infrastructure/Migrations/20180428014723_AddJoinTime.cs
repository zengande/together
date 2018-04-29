using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddJoinTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoinTime",
                table: "participant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 28, 9, 47, 23, 162, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 28, 9, 38, 36, 460, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinTime",
                table: "participant");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 28, 9, 38, 36, 460, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 28, 9, 47, 23, 162, DateTimeKind.Local));
        }
    }
}
