using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItCubeVoteDb.Migrations
{
    public partial class ChangeDateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Dates_DateId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Dates_DateId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.RenameColumn(
                name: "DateId",
                table: "Votes",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_DateId",
                table: "Votes",
                newName: "IX_Votes_EventId");

            migrationBuilder.RenameColumn(
                name: "DateId",
                table: "Projects",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_DateId",
                table: "Projects",
                newName: "IX_Projects_EventId");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Events_EventId",
                table: "Projects",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Events_EventId",
                table: "Votes",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Events_EventId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Events_EventId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Votes",
                newName: "DateId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_EventId",
                table: "Votes",
                newName: "IX_Votes_DateId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Projects",
                newName: "DateId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_EventId",
                table: "Projects",
                newName: "IX_Projects_DateId");

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Dates_DateId",
                table: "Projects",
                column: "DateId",
                principalTable: "Dates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Dates_DateId",
                table: "Votes",
                column: "DateId",
                principalTable: "Dates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
