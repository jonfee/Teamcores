using System;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 章节详细信息
	/// </summary>
	public class ChapterDetails
	{
		/// <summary>
		/// 章节ID
		/// </summary>
		public long ChapterId { get; set; }

		/// <summary>
		/// 章节归属课程
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 归属课程名称
		/// </summary>
		public string CourseTitle { get; set; }

		/// <summary>
		/// 章节父节点
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// 章节父节点的标题
		/// </summary>
		public string ParentTitle { get; set; }

		/// <summary>
		/// 章节标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 章节内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 章节视频
		/// </summary>
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
		/// 章节建立时间
		/// </summary>
		public DateTime CreateTime { get; set; }
	}
}
