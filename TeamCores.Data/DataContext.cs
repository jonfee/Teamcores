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
            var connection = "Data Source=.;Initial Catalog=Dev.Teamcores;Persist Security Info=True;User ID=sa;Password=sql1.1";
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
        /// 配置信息
        /// </summary>
        public DbSet<Options> Options { get; set; }
    }
}
