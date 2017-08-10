using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCores.Data.Migrations
{
    public partial class modifyColumn_CourseId_to_CourseIds_for_Exams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "CourseIds",
                table: "Exams",
                type: "varchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseIds",
                table: "Exams");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Exams",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
