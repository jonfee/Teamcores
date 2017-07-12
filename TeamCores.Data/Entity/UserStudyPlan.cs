using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 用户学习计划进度
    /// </summary>
    [Table("UserStudyPlan")]
    public class UserStudyPlan
    {
        /// <summary>
        /// 计划ID
        /// </summary>
        [Key]
        public long PlanId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public long UserId { get; set; }

        /// <summary>
        /// 学习状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 学习进度
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新状态时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
    }
}
