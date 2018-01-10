using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProcessAPI.Migrations
{
    public partial class fixedfkstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_StudentExam_StudentExamID",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentExam_StudentExamID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_StudentExamID",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Exam_StudentExamID",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "StudentExamID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentExamID",
                table: "Exam");

            migrationBuilder.AddColumn<int>(
                name: "ExamID",
                table: "StudentExam",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "StudentExam",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_ExamID",
                table: "StudentExam",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_StudentID",
                table: "StudentExam",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExam_Exam_ExamID",
                table: "StudentExam",
                column: "ExamID",
                principalTable: "Exam",
                principalColumn: "ExamID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExam_Student_StudentID",
                table: "StudentExam",
                column: "StudentID",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentExam_Exam_ExamID",
                table: "StudentExam");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExam_Student_StudentID",
                table: "StudentExam");

            migrationBuilder.DropIndex(
                name: "IX_StudentExam_ExamID",
                table: "StudentExam");

            migrationBuilder.DropIndex(
                name: "IX_StudentExam_StudentID",
                table: "StudentExam");

            migrationBuilder.DropColumn(
                name: "ExamID",
                table: "StudentExam");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "StudentExam");

            migrationBuilder.AddColumn<int>(
                name: "StudentExamID",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentExamID",
                table: "Exam",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentExamID",
                table: "Student",
                column: "StudentExamID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentExam_StudentExamID",
                table: "Student",
                column: "StudentExamID",
                principalTable: "StudentExam",
                principalColumn: "StudentExamID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
