using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "activityseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "participantseq",
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
                    OwnerId = table.Column<int>(nullable: true),
                    ActivityStatusId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2018, 4, 28, 9, 38, 36, 460, DateTimeKind.Local)),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ActivityTime = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    LimitsNum = table.Column<int>(nullable: true)
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
                    Id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(maxLength: 200, nullable: true),
                    ActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participant", x => x.Id);
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

            migrationBuilder.DropSequence(
                name: "participantseq");
        }
    }
}
