using System;
using System.Collections.Generic;
using TeamCores.Domain.Models.StudyPlan;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 学习计划详细信息
	/// </summary>
	public class StudyPlanDetails
	{
		public long PlanId { get; set; }

		/// <summary>
		/// 制定学习计划用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 学员数量
		/// </summary>
		public int StudentCount { get; set; }

		/// <summary>
		/// 学习计划标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 计划说明
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 学员集合
		/// </summary>
		public List<Student> Students { get; set; }

		/// <summary>
		/// 课程集合
		/// </summary>
		public List<CourseInfo> Courses { get; set; }
	}
}
