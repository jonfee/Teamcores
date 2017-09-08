using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Models
{
	/// <summary>
	/// 题目简要信息
	/// </summary>
	public class QuestionSimpleInfo
	{
		public long QuestionId { get; set; }

		/// <summary>
		/// 题目类型（单选，多选，对错，填空题，问答题）
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// 题目
		/// </summary>
		public string Topic { get; set; }

		/// <summary>
		/// 题目状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 归属课程ID
		/// </summary>
		public long CourseId { get; set; }
	}
}
