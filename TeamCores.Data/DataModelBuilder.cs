using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;

namespace TeamCores.Data
{
    public class DataModelBuilder
    {
        public static void ModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(p => p.UserId);
            modelBuilder.Entity<Options>().HasKey(p => p.OptionId);
            modelBuilder.Entity<UserStudy>().HasKey(p => p.UserId);
            modelBuilder.Entity<Messages>().HasKey(p => p.MessageId);
            modelBuilder.Entity<Subjects>().HasKey(p => p.SubjectId);
            modelBuilder.Entity<Course>().HasKey(p => p.CourseId);
            modelBuilder.Entity<Chapter>().HasKey(p => p.ChapterId);
            modelBuilder.Entity<Exams>().HasKey(p => p.ExamId);
            modelBuilder.Entity<ExamUsers>().HasKey(p => p.Id);
            modelBuilder.Entity<Questions>().HasKey(p => p.QuestionId);
            modelBuilder.Entity<StudyPlan>().HasKey(p => p.PlanId);
            modelBuilder.Entity<StudyRecord>().HasKey(p => p.RecordId);
            modelBuilder.Entity<UserStudyPlan>().HasKey(p => new { p.PlanId, p.UserId });
        }
    }
}
