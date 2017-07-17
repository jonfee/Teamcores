using System;
using System.ComponentModel.DataAnnotations;
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
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long PlanId { get; set; }

        /// <summary>
        /// 制定学习计划用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 学员数量
        /// </summary>
        public int Student { get; set; }

        /// <summary>
        /// 学习计划标题
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        /// <summary>
        /// 计划说明
        /// </summary>
        [Column(TypeName = "nvarchar(250)")]
        public string Content { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
