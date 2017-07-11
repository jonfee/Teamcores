using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 考卷使用用户
    /// </summary>
    public class ExamUsers
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 考试题目ID
        /// </summary>
        public long ExamId { get; set; }

        /// <summary>
        /// 答卷总计时间
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 答案，JSON格式
        /// </summary>
        public string Answer { get; set; }



        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 交卷时间
        /// </summary>
        public DateTime? PostTime { get; set; }

        /// <summary>
        /// 阅卷状态
        /// </summary>
        public int MarkingStatus { get; set; }

        /// <summary>
        /// 阅卷时间
        /// </summary>
        public DateTime? MarkingTime { get; set; }
    }
}
