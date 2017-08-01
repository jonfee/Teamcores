using Microsoft.EntityFrameworkCore;
using System;
using TeamCores.Data.Entity;

namespace TeamCores.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connection = MiddlewareConfig.Configuration["Database:Connection"];
            //bool develop = Convert.ToBoolean(MiddlewareConfig.Configuration["Develop"]);
            //if (develop)
            //{
            //    connection = MiddlewareConfig.Configuration["Database:DevConnection"];
            //}
            //TODO: 正式发布前需要注释
            var connection = "Data Source=47.52.35.179;Initial Catalog=Dev.Teamcores;Persist Security Info=True;User ID=teamcores;Password=team1.1";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataModelBuilder.ModelCreating(modelBuilder);
        }

        /// <summary>
        /// 用户账户信息
        /// </summary>
        public DbSet<Users> Users { get; set; }

        /// <summary>
        /// 用户学习状态
        /// </summary>
        public DbSet<UserStudy> UserStudy { get; set; }

        /// <summary>
        /// 科目
        /// </summary>
        public DbSet<Subjects> Subjects { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public DbSet<Course> Course { get; set; }

        /// <summary>
        /// 章节
        /// </summary>
        public DbSet<Chapter> Chapter { get; set; }

        /// <summary>
        /// 考卷
        /// </summary>
        public DbSet<Exams> Exams { get; set; }

        /// <summary>
        /// 用户考试记录
        /// </summary>
        public DbSet<ExamUsers> ExamUsers { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public DbSet<Questions> Questions { get; set; }

        /// <summary>
        /// 学习计划
        /// </summary>
        public DbSet<StudyPlan> StudyPlan { get; set; }

		/// <summary>
		/// 学习计划关联的课程
		/// </summary>
		public DbSet<StudyPlanCourse> StudyPlanCourse { get; set; }

        /// <summary>
        /// 学习记录
        /// </summary>
        public DbSet<StudyRecord> StudyRecord { get; set; }

        /// <summary>
        /// 用户学习计划进度
        /// </summary>
        public DbSet<UserStudyPlan> UserStudyPlan { get; set; }

        /// <summary>
        /// 配置信息
        /// </summary>
        public DbSet<Options> Options { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public DbSet<Messages> Messages { get; set; }
    }
}
