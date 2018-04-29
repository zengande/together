using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddParticipantSexColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "participant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 29, 21, 22, 5, 426, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 28, 9, 47, 23, 162, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "participant");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 28, 9, 47, 23, 162, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 29, 21, 22, 5, 426, DateTimeKind.Local));
        }
    }
}
