using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class addAddressVisibleRuleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activities_addressvisiblerules_AddressVisibleRuleId",
                table: "activities");

            migrationBuilder.AlterColumn<int>(
                name: "AddressVisibleRuleId",
                table: "activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_activities_addressvisiblerules_AddressVisibleRuleId",
                table: "activities",
                column: "AddressVisibleRuleId",
                principalTable: "addressvisiblerules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activities_addressvisiblerules_AddressVisibleRuleId",
                table: "activities");

            migrationBuilder.AlterColumn<int>(
                name: "AddressVisibleRuleId",
                table: "activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_activities_addressvisiblerules_AddressVisibleRuleId",
                table: "activities",
                column: "AddressVisibleRuleId",
                principalTable: "addressvisiblerules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
