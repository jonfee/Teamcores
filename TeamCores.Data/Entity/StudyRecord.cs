using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
	/// <summary>
	/// 学习记录
	/// </summary>
	[Table("StudyRecord")]
    public class StudyRecord
    {
		/// <summary>
		/// 学习记录ID
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
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
		/// 课程章节ID
		/// </summary>
		public long ChapterId { get; set; }

		/// <summary>
		/// 阅读次数
		/// </summary>
		public int ReadCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后学习时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
    }
}
