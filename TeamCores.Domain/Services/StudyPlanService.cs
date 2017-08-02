using System.Collections.Generic;
using TeamCores.Domain.Models.StudyPlan;
using TeamCores.Domain.Output;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 学习计划领域业务服务
	/// </summary>
	public class StudyPlanService
	{
		/// <summary>
		/// 添加新学习计划
		/// </summary>
		/// <param name="userId">计划制定者的ID</param>
		/// <param name="title">计划标题</param>
		/// <param name="content">计划内容说明</param>
		/// <param name="courseIds">关联的课程ID集合</param>
		/// <param name="studentIds">关联的学员ID集合</param>
		/// <returns></returns>
		public bool Add(long userId, string title, string content, IEnumerable<long> courseIds, IEnumerable<long> studentIds)
		{
			var newPlan = new NewStudyPlan
			{
				Content = content,
				CourseIds = courseIds,
				Students = studentIds,
				Title = title,
				UserId = userId
			};

			return newPlan.Save();
		}

		/// <summary>
		/// 搜索学习计划信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="keyword"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public PagerModel<Data.Entity.StudyPlan> Search(int pageSize, int pageIndex, string keyword, int? status)
		{
			StudyPlanSearch search = new StudyPlanSearch(pageIndex, pageSize, keyword, status);

			return search.Search();
		}

		/// <summary>
		/// 设置状态为“启用”
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		public bool SetEnable(long planId)
		{
			var plan = new StudyPlanEditor(planId);

			return plan.CanSetEnable();
		}

		/// <summary>
		/// 设置状态为“禁用”
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		public bool SetDisable(long planId)
		{
			var plan = new StudyPlanEditor(planId);

			return plan.SetDisable();
		}

		/// <summary>
		/// 获取学习计划的详细信息
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		public StudyPlanDetails GetStudyPlanDetails(long planId)
		{
			var plan = new StudyPlanEditor(planId);
			//获取学员
			plan.GetStudents();
			//获取课程
			plan.GetCourses();

			return new StudyPlanDetails
			{
				PlanId = plan.ID,
				Title = plan.StudyPlan.Title,
				Content = plan.StudyPlan.Content,
				Status = plan.StudyPlan.Status,
				StudentCount = plan.StudyPlan.Student,
				UserId = plan.StudyPlan.UserId,
				CreateTime = plan.StudyPlan.CreateTime,
				Students = plan.Students,
				Courses = plan.Courses
			};
		}

		/// <summary>
		/// 获取学员学习计划详细信息
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <param name="userId">学员ID</param>
		/// <returns></returns>
		public StudentStudyPlanDetails GetUserStudyPlanDetails(long planId, long userId)
		{
			var plan = new StudyPlanEditor(planId);

			//获取课程
			plan.GetCourses();

			var student = plan.GetStudent(userId);

			return new StudentStudyPlanDetails
			{
				PlanId = plan.ID,
				Title = plan.StudyPlan.Title,
				Content = plan.StudyPlan.Content,
				Status = plan.StudyPlan.Status,
				StudentCount = plan.StudyPlan.Student,
				UserId = plan.StudyPlan.UserId,
				CreateTime = plan.StudyPlan.CreateTime,
				Student = student,
				Courses = plan.Courses
			};
		}
	}
}
