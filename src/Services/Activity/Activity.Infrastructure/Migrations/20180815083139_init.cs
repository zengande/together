using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "activityseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "activitystatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activitystatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    OwnerId = table.Column<string>(maxLength: 200, nullable: true),
                    ActivityStatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndRegisterTime = table.Column<DateTime>(nullable: false),
                    ActivityStartTime = table.Column<DateTime>(nullable: false),
                    Address_DetailAddress = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_County = table.Column<string>(nullable: true),
                    Address_Province = table.Column<string>(nullable: true),
                    Address_Location = table.Column<string>(nullable: true),
                    LimitsNum = table.Column<int>(nullable: true),
                    Funds = table.Column<decimal>(nullable: true),
                    ActivityEndTime = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_activities_activitystatus_ActivityStatusId",
                        column: x => x.ActivityStatusId,
                        principalTable: "activitystatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participant",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 200, nullable: false),
                    Nickname = table.Column<string>(maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(maxLength: 200, nullable: true),
                    ActivityId = table.Column<int>(nullable: false),
                    Sex = table.Column<int>(nullable: false, defaultValue: 0),
                    JoinTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participant", x => new { x.UserId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_participant_activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_ActivityStatusId",
                table: "activities",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_participant_ActivityId",
                table: "participant",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participant");

            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropTable(
                name: "activitystatus");

            migrationBuilder.DropSequence(
                name: "activityseq");
        }
    }
}
