using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProcessAPI.Migrations
{
    public partial class @fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinishedID",
                table: "StudentExam",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Finished",
                columns: table => new
                {
                    FinishedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finished", x => x.FinishedID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_FinishedID",
                table: "StudentExam",
                column: "FinishedID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExam_Finished_FinishedID",
                table: "StudentExam",
                column: "FinishedID",
                principalTable: "Finished",
                principalColumn: "FinishedID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentExam_Finished_FinishedID",
                table: "StudentExam");

            migrationBuilder.DropTable(
                name: "Finished");

            migrationBuilder.DropIndex(
                name: "IX_StudentExam_FinishedID",
                table: "StudentExam");

            migrationBuilder.DropColumn(
                name: "FinishedID",
                table: "StudentExam");
        }
    }
}
