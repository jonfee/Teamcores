using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamCores.Data.Migrations
{
    public partial class v010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    ChapterId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ParentId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.ChapterId);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Objective = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SubjectId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamId = table.Column<long>(nullable: false),
                    Answers = table.Column<int>(nullable: false),
                    Ask = table.Column<int>(nullable: false),
                    AskTotal = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExamType = table.Column<int>(nullable: false),
                    Filling = table.Column<int>(nullable: false),
                    FillingTotal = table.Column<int>(nullable: false),
                    Judge = table.Column<int>(nullable: false),
                    JudgeTotal = table.Column<int>(nullable: false),
                    Multiple = table.Column<int>(nullable: false),
                    MultipleTotal = table.Column<int>(nullable: false),
                    Pass = table.Column<int>(nullable: false),
                    Questions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Radio = table.Column<int>(nullable: false),
                    RedioTotal = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Total = table.Column<int>(nullable: false),
                    UseCount = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamId);
                });

            migrationBuilder.CreateTable(
                name: "ExamUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExamId = table.Column<long>(nullable: false),
                    MarkingStatus = table.Column<int>(nullable: false),
                    MarkingTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    PostTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Times = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReadTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Readed = table.Column<bool>(nullable: false),
                    Receiver = table.Column<long>(nullable: false),
                    Sender = table.Column<long>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<long>(nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Marking = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SubjectId = table.Column<long>(nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Type = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlan",
                columns: table => new
                {
                    PlanId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Student = table.Column<int>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlan", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "StudyRecord",
                columns: table => new
                {
                    RecordId = table.Column<long>(nullable: false),
                    CourseId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReadCount = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyRecord", x => x.RecordId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<long>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "UserStudyPlan",
                columns: table => new
                {
                    PlanId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Progress = table.Column<float>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStudyPlan", x => new { x.PlanId, x.UserId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "ExamUsers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "StudyPlan");

            migrationBuilder.DropTable(
                name: "StudyRecord");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "UserStudyPlan");
        }
    }
}
