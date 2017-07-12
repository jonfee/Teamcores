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
        }
    }
}
