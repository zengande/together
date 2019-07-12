using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddIsOwnerCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "participant",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "participant");
        }
    }
}
