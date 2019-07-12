using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class addAddressVisibleRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Funds",
                table: "activities");

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "categories",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressVisibleRuleId",
                table: "activities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "addressvisiblerules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addressvisiblerules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_AddressVisibleRuleId",
                table: "activities",
                column: "AddressVisibleRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_activities_addressvisiblerules_AddressVisibleRuleId",
                table: "activities",
                column: "AddressVisibleRuleId",
                principalTable: "addressvisiblerules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activities_addressvisiblerules_AddressVisibleRuleId",
                table: "activities");

            migrationBuilder.DropTable(
                name: "addressvisiblerules");

            migrationBuilder.DropIndex(
                name: "IX_activities_AddressVisibleRuleId",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "AddressVisibleRuleId",
                table: "activities");

            migrationBuilder.AddColumn<decimal>(
                name: "Funds",
                table: "activities",
                nullable: true);
        }
    }
}
