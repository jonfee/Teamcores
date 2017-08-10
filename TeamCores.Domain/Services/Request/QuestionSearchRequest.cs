using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Services.Request
{
	/// <summary>
	/// 题目搜索请求
	/// </summary>
	public class QuestionSearchRequest
	{
		/// <summary>
		/// 关联的课程ID
		/// </summary>
		public long? CourseId { get; set; }

		/// <summary>
		/// 题目类型
		/// </summary>
		public int? QuestionType { get; set; }

		/// <summary>
		/// 指定的题目ID集合
		/// </summary>
		public IEnumerable<long> QuestionIds { get; set; }

		/// <summary>
		/// 题目标题关健词
		/// </summary>
		public string Keyword { get; set; }

		/// <summary>
		/// 题目状态
		/// </summary>
		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
