using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProcessAPI.Migrations
{
    public partial class changedtimestampname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Process");

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Process",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Process");

            migrationBuilder.AddColumn<string>(
                name: "TimeStamp",
                table: "Process",
                nullable: true);
        }
    }
}
