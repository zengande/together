using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddClientRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "AppActivities");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "AppActivities");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "AppActivities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "AppActivities",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRequests");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "AppActivities");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "AppActivities");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "AppActivities",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "AppActivities",
                type: "varchar(200) CHARACTER SET utf8mb4",
                maxLength: 200,
                nullable: true);
        }
    }
}
