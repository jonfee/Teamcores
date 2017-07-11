using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 学习计划
    /// </summary>
    [Table("StudyPlan")]
    public class StudyPlan
    {
        /// <summary>
        /// 计划ID
        /// </summary>
        public long PlanId { get; set; }

        /// <summary>
        /// 制定学习计划用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 学员用户ID
        /// </summary>
        public long Student { get; set; }

        /// <summary>
        /// 学习计划标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 计划说明
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
