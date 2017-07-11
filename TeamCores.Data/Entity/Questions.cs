using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 题库
    /// </summary>
    [Table("Questions")]
    public class Questions
    {
        /// <summary>
        /// 题目ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long QuestionId { get; set; }

        /// <summary>
        /// 题目建立用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 归属课程ID
        /// </summary>
        public long CourseId { get; set; }

        /// <summary>
        /// 科目ID
        /// </summary>
        public long SubjectId { get; set; }

        /// <summary>
        /// 课程类型（单选，多选，对错，填空题，问答题）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否需要阅卷
        /// </summary>
        public bool Marking { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 题目选项
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 题目状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后使用时间
        /// </summary>
        public DateTime LastTime { get; set; }
    }
}
