using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 在线学习科目
    /// </summary>
    [Table("Subjects")]
    public class Subjects
    {
        public long SubjectId { get; set; }

        /// <summary>
        /// 学科名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程数量
        /// </summary>
        public int Count { get; set; }
    }
}
