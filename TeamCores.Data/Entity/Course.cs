using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 课程
    /// </summary>
    [Table("Course")]
    public class Course
    {
        public long CourseId { get; set; }

        public long SubjectId { get; set; }
    }
}
