using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProcessAPI.Migrations
{
    public partial class fixedstudentexamtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentExamID",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentExamID",
                table: "Student",
                column: "StudentExamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentExam_StudentExamID",
                table: "Student",
                column: "StudentExamID",
                principalTable: "StudentExam",
                principalColumn: "StudentExamID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentExam_StudentExamID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_StudentExamID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentExamID",
                table: "Student");
        }
    }
}
