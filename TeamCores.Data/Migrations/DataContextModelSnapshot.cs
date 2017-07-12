using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TeamCores.Data;

namespace TeamCores.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamCores.Data.Entity.UserStudy", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Answers");

                    b.Property<int>("Average");

                    b.Property<int>("ReadCount");

                    b.Property<int>("StudyPlans");

                    b.Property<int>("StudyTimes");

                    b.Property<int>("TestExams");

                    b.HasKey("UserId");

                    b.ToTable("UserStudy");
                });
        }
    }
}
