using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class modifiedAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_County",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "Address_Location",
                table: "activities");

            migrationBuilder.RenameColumn(
                name: "Address_Province",
                table: "activities",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "Address_DetailAddress",
                table: "activities",
                newName: "DetailAddress");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "activities",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "activities",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DetailAddress",
                table: "activities",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "activities",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "activities",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "activities",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "activities");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "activities");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "activities",
                newName: "Address_Province");

            migrationBuilder.RenameColumn(
                name: "DetailAddress",
                table: "activities",
                newName: "Address_DetailAddress");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "activities",
                newName: "Address_City");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Province",
                table: "activities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_DetailAddress",
                table: "activities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_City",
                table: "activities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Address_County",
                table: "activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Location",
                table: "activities",
                nullable: true);
        }
    }
}
