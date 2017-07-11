using System;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 考试题目
    /// </summary>
    public class Exams
    {
        /// <summary>
        /// 考试题目ID
        /// </summary>
        public long ExamId { get; set; }

        /// <summary>
        /// 考试类型
        /// </summary>
        public int ExamType { get; set; }

        /// <summary>
        /// 考试标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 考题ID集合
        /// </summary>
        public string Questions { get; set; }

        /// <summary>
        /// 考题状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 考题创建用户
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 考题使用次数
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 答卷数量
        /// </summary>
        public int Answers { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
