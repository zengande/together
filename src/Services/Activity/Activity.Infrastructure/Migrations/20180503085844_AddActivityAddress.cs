using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddActivityAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "activities");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_County",
                table: "activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_DetailAddress",
                table: "activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Location",
                table: "activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Province",
                table: "activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "Address_County",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "Address_DetailAddress",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "Address_Location",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "Address_Province",
                table: "activities");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "activities",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
