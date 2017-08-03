using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TeamCores.Data;

namespace TeamCores.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170803040215_addColumn_ParentPath_for_Chapter")]
    partial class addColumn_ParentPath_for_Chapter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamCores.Data.Entity.Chapter", b =>
                {
                    b.Property<long>("ChapterId");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count");

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsLeaf");

                    b.Property<long>("ParentId");

                    b.Property<string>("ParentPath")
                        .HasColumnType("varchar(max)");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ChapterId");

                    b.ToTable("Chapter");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.Course", b =>
                {
                    b.Property<long>("CourseId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Image");

                    b.Property<string>("Objective");

                    b.Property<string>("Remarks");

                    b.Property<int>("Status");

                    b.Property<long>("SubjectId");

                    b.Property<string>("Title");

                    b.Property<long>("UserId");

                    b.HasKey("CourseId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.Exams", b =>
                {
                    b.Property<long>("ExamId");

                    b.Property<int>("Answers");

                    b.Property<int>("Ask");

                    b.Property<int>("AskTotal");

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<int>("ExamType");

                    b.Property<int>("Filling");

                    b.Property<int>("FillingTotal");

                    b.Property<int>("Judge");

                    b.Property<int>("JudgeTotal");

                    b.Property<int>("Multiple");

                    b.Property<int>("MultipleTotal");

                    b.Property<int>("Pass");

                    b.Property<string>("Questions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Radio");

                    b.Property<int>("RedioTotal");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Status");

                    b.Property<int>("Time");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Total");

                    b.Property<int>("UseCount");

                    b.Property<long>("UserId");

                    b.HasKey("ExamId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.ExamUsers", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<long>("ExamId");

                    b.Property<int>("MarkingStatus");

                    b.Property<DateTime?>("MarkingTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("PostTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Times");

                    b.Property<int>("Total");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ExamUsers");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.Messages", b =>
                {
                    b.Property<long>("MessageId");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ReadTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("Readed");

                    b.Property<long>("Receiver");

                    b.Property<long>("Sender");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MessageId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.Options", b =>
                {
                    b.Property<long>("OptionId");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OptionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.Questions", b =>
                {
                    b.Property<long>("QuestionId");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count");

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("Marking");

                    b.Property<int>("Status");

                    b.Property<long>("SubjectId");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Type");

                    b.Property<long>("UserId");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.StudyPlan", b =>
                {
                    b.Property<long>("PlanId");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Status");

                    b.Property<int>("Student");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("UserId");

                    b.HasKey("PlanId");

                    b.ToTable("StudyPlan");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.StudyPlanCourse", b =>
                {
                    b.Property<long>("PlanId");

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Sort");

                    b.HasKey("PlanId", "CourseId");

                    b.HasAlternateKey("CourseId", "PlanId");

                    b.ToTable("StudyPlanCourse");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.StudyRecord", b =>
                {
                    b.Property<long>("RecordId");

                    b.Property<long>("ChapterId");

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<int>("ReadCount");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime");

                    b.Property<long>("UserId");

                    b.HasKey("RecordId");

                    b.ToTable("StudyRecord");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.Subjects", b =>
                {
                    b.Property<long>("SubjectId");

                    b.Property<int>("Count");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Status");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.Users", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("LastTime")
                        .HasColumnType("datetime");

                    b.Property<int>("LoginCount");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.UserStudy", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<int>("Answers");

                    b.Property<int>("Average");

                    b.Property<int>("ReadCount");

                    b.Property<int>("StudyPlans");

                    b.Property<int>("StudyTimes");

                    b.Property<int>("TestExams");

                    b.HasKey("UserId");

                    b.ToTable("UserStudy");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.UserStudyPlan", b =>
                {
                    b.Property<long>("PlanId");

                    b.Property<long>("UserId");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<float>("Progress");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime");

                    b.HasKey("PlanId", "UserId");

                    b.ToTable("UserStudyPlan");
                });
        }
    }
}
