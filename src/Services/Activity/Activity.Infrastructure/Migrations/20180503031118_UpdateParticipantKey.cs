using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Activity.Infrastructure.Migrations
{
    public partial class UpdateParticipantKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_participant",
                table: "participant");

            migrationBuilder.DropSequence(
                name: "participantseq");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "participant");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "participant",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "activities",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_participant",
                table: "participant",
                columns: new[] { "UserId", "ActivityId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_participant",
                table: "participant");

            migrationBuilder.CreateSequence(
                name: "participantseq",
                incrementBy: 10);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "participant",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "participant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "activities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_participant",
                table: "participant",
                column: "Id");
        }
    }
}
