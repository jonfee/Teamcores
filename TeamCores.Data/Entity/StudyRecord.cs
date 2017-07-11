using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 学习记录
    /// </summary>
    public class StudyRecord
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public long RecordId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public long CourseId { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
