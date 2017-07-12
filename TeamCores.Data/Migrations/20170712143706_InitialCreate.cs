using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamCores.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    LastTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LoginCount = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(type: "varchar(32)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserStudy",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answers = table.Column<int>(nullable: false),
                    Average = table.Column<int>(nullable: false),
                    ReadCount = table.Column<int>(nullable: false),
                    StudyPlans = table.Column<int>(nullable: false),
                    StudyTimes = table.Column<int>(nullable: false),
                    TestExams = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStudy", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserStudy");
        }
    }
}
