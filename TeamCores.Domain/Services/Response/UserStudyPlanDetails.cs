using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Models.StudyPlan;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 用户学习计划搜索结果
	/// </summary>
	public class UserStudyPlanDetails
	{
		/// <summary>
		/// 用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 学习计划ID
		/// </summary>
		public long PlanId { get; set; }

		/// <summary>
		/// 制定学习计划用户ID
		/// </summary>
		public long CreatorId { get; set; }

        /// <summary>
        /// 制定学习计划的用户名称
        /// </summary>
        public string CreatorName { get; set; }

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
		/// 学习计划状态
		/// </summary>
		public int PlanStatus { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 学习状态
		/// </summary>
		public int StudyStatus { get; set; }

		/// <summary>
		/// 学习进度
		/// </summary>
		public float Progress { get; set; }

		/// <summary>
		/// 最后一次开始学习的时间
		/// </summary>
		public DateTime? LastStudyTime { get; set; }

		/// <summary>
		/// 课程集合
		/// </summary>
		public List<CourseInfo> Courses { get; set; }

        /// <summary>
        /// 学员集合
        /// </summary>
        public List<Student> Students { get; set; }
	}
}
