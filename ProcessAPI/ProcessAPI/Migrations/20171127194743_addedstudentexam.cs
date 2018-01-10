using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProcessAPI.Migrations
{
    public partial class addedstudentexam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exam",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Exam");

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ExamID",
                table: "Exam",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "StudentExamID",
                table: "Exam",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exam",
                table: "Exam",
                column: "ExamID");

            migrationBuilder.CreateTable(
                name: "StudentExam",
                columns: table => new
                {
                    StudentExamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExam", x => x.StudentExamID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exam_StudentExamID",
                table: "Exam",
                column: "StudentExamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_StudentExam_StudentExamID",
                table: "Exam",
                column: "StudentExamID",
                principalTable: "StudentExam",
                principalColumn: "StudentExamID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_StudentExam_StudentExamID",
                table: "Exam");

            migrationBuilder.DropTable(
                name: "StudentExam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exam",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_StudentExamID",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ExamID",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "StudentExamID",
                table: "Exam");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Student",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Exam",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exam",
                table: "Exam",
                column: "ID");
        }
    }
}
