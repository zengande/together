using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class UpdateCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AppCatalogs");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AppCatalogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "AppCatalogs");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "AppCatalogs",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
