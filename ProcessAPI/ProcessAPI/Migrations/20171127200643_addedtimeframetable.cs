using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProcessAPI.Migrations
{
    public partial class addedtimeframetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeFrame",
                columns: table => new
                {
                    TimeFrameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentExamID = table.Column<int>(type: "int", nullable: true),
                    TabTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindowName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrame", x => x.TimeFrameID);
                    table.ForeignKey(
                        name: "FK_TimeFrame_StudentExam_StudentExamID",
                        column: x => x.StudentExamID,
                        principalTable: "StudentExam",
                        principalColumn: "StudentExamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeFrame_StudentExamID",
                table: "TimeFrame",
                column: "StudentExamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrame");
        }
    }
}
