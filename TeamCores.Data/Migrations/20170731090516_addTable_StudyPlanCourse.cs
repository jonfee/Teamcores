using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamCores.Data.Migrations
{
    public partial class addTable_StudyPlanCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyPlanCourse",
                columns: table => new
                {
                    PlanId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlanCourse", x => new { x.PlanId, x.CourseId });
                    table.UniqueConstraint("AK_StudyPlanCourse_CourseId_PlanId", x => new { x.CourseId, x.PlanId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyPlanCourse");
        }
    }
}
