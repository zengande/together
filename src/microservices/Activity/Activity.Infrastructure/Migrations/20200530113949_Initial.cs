﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppActivityStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppActivityStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppAddressVisibleRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAddressVisibleRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppActivities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<string>(maxLength: 200, nullable: true),
                    ActivityStatusId = table.Column<int>(nullable: false),
                    AddressVisibleRuleId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndRegisterTime = table.Column<DateTime>(nullable: false),
                    ActivityStartTime = table.Column<DateTime>(nullable: false),
                    DetailAddress = table.Column<string>(maxLength: 512, nullable: true),
                    City = table.Column<string>(maxLength: 200, nullable: true),
                    Province = table.Column<string>(maxLength: 200, nullable: true),
                    Longitude = table.Column<double>(nullable: true, defaultValue: 0.0),
                    Latitude = table.Column<double>(nullable: true, defaultValue: 0.0),
                    LimitsNum = table.Column<int>(nullable: true),
                    ActivityEndTime = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppActivities_AppActivityStatus_ActivityStatusId",
                        column: x => x.ActivityStatusId,
                        principalTable: "AppActivityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppActivities_AppAddressVisibleRules_AddressVisibleRuleId",
                        column: x => x.AddressVisibleRuleId,
                        principalTable: "AppAddressVisibleRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppParticipants",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 200, nullable: false),
                    ActivityId = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(maxLength: 200, nullable: true),
                    Sex = table.Column<int>(nullable: false, defaultValue: 0),
                    JoinTime = table.Column<DateTime>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParticipants", x => new { x.UserId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_AppParticipants_AppActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "AppActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppActivityStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "RECRUITMENT" },
                    { 2, "PROCESSING" },
                    { 3, "FINISHED" },
                    { 4, "TIMEOUT" },
                    { 5, "OBSOLETED" }
                });

            migrationBuilder.InsertData(
                table: "AppAddressVisibleRules",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PUBLICVISIBLE" },
                    { 2, "PARTICIPANTSVISIBLE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppActivities_ActivityStatusId",
                table: "AppActivities",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AppActivities_AddressVisibleRuleId",
                table: "AppActivities",
                column: "AddressVisibleRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppParticipants_ActivityId",
                table: "AppParticipants",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppParticipants");

            migrationBuilder.DropTable(
                name: "AppActivities");

            migrationBuilder.DropTable(
                name: "AppActivityStatus");

            migrationBuilder.DropTable(
                name: "AppAddressVisibleRules");
        }
    }
}