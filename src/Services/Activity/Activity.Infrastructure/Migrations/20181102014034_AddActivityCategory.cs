using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class AddActivityCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "categoryseq",
                incrementBy: 10);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "activities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Key = table.Column<string>(maxLength: 150, nullable: true),
                    Text = table.Column<string>(maxLength: 150, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Sort = table.Column<int>(nullable: false, defaultValue: 0),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categories_categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_CategoryId",
                table: "activities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentId",
                table: "categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_activities_categories_CategoryId",
                table: "activities",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_activities_categories_CategoryId",
                table: "activities");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropIndex(
                name: "IX_activities_CategoryId",
                table: "activities");

            migrationBuilder.DropSequence(
                name: "categoryseq");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "activities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
