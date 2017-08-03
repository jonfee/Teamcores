using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
	/// <summary>
	/// 课程
	/// </summary>
	[Table("Course")]
    public class Course
    {
		/// <summary>
		/// 课程ID
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long CourseId { get; set; }

        /// <summary>
        /// 归属科目
        /// </summary>
        public long SubjectId { get; set; }

        /// <summary>
        /// 建立课程用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 课程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 课程封面图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 学习目标
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// 课程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
