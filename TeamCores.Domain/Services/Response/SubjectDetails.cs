using System;
using System.Collections.Generic;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 科目详细信息
	/// </summary>
	public class SubjectDetails
	{
		public long SubjectId { get; set; }

		/// <summary>
		/// 学科名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 课程数量
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// 科目状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 科目下的课程集合
		/// </summary>
		public List<Data.Entity.Course> Courses { get; set; }
	}
}
