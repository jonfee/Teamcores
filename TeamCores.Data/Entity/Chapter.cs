using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 课程章节
    /// </summary>
    [Table("Chapter")]
    public class Chapter
    {
		/// <summary>
		/// 章节ID
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ChapterId { get; set; }

        /// <summary>
        /// 章节归属课程
        /// </summary>
        public long CourseId { get; set; }

        /// <summary>
        /// 章节节点
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 章节标题
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        /// <summary>
        /// 章节内容
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        /// <summary>
        /// 章节视频
        /// </summary>
        [Column(TypeName = "nvarchar(250)")]
        public string Video { get; set; }

        /// <summary>
        /// 章节学习次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 章节状态
        /// </summary>
        public int Status { get; set; }

		/// <summary>
		/// 是否为叶子章节
		/// </summary>
		public bool IsLeaf { get; set; }

        /// <summary>
        /// 章节建立时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

    }
}
