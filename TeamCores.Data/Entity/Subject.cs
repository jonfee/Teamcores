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
        /// <summary>
        /// 科目ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SubjectId { get; set; }

        /// <summary>
        /// 学科名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 科目状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
