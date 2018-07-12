using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddActivityFundsColunm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 30, 13, 48, 30, 903, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 29, 21, 22, 5, 426, DateTimeKind.Local));

            migrationBuilder.AddColumn<decimal>(
                name: "Funds",
                table: "activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Funds",
                table: "activities");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "activities",
                nullable: false,
                defaultValue: new DateTime(2018, 4, 29, 21, 22, 5, 426, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 4, 30, 13, 48, 30, 903, DateTimeKind.Local));
        }
    }
}
