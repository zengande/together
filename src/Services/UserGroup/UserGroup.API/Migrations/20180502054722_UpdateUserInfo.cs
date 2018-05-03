using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Together.UserGroup.API.Migrations
{
    public partial class UpdateUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "users",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySQL:AutoIncrement", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200)
                .Annotation("MySQL:AutoIncrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "users",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
