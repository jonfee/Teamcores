using System;
using System.Collections.Generic;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 课程详细信息
	/// </summary>
	public class CourseDetails
	{
		/// <summary>
		/// 课程ID
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 归属科目
		/// </summary>
		public long SubjectId { get; set; }

		/// <summary>
		/// 归属科目名称
		/// </summary>
		public string SubjectName { get; set; }

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

		/// <summary>
		/// 所有章节
		/// </summary>
		public List<Data.Entity.Chapter> Chapters { get; set; }
	}
}
