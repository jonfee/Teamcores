using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Models.StudyPlan;
using TeamCores.Domain.Models.UserStuding;
using TeamCores.Domain.Output;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	public class UserStudyPlanService
	{
		/// <summary>
		/// 搜索用户的学习计划信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="studyStatus">学习状态，为NULL时忽略</param>
		/// <returns></returns>
		public PagerModel<UserStudyPlanSearchResultItem> Search(int pageSize, int pageIndex, int? studyStatus)
		{
			UserStudyPlanSearch search = new UserStudyPlanSearch(pageIndex, pageSize, studyStatus);

			return search.Search();
		}

		/// <summary>
		/// 获取用户学习计划执行详细
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="planId">计划ID</param>
		/// <returns></returns>
		public UserStudyPlanDetails GetPlanDetails(long userId, long planId)
		{
			var plan = new StudyPlanEditor(planId);

			//获取指定学员的信息
			var student = plan.GetStudent(userId);

			//获取学习计划中的课程信息
			plan.GetCourses();

			return new UserStudyPlanDetails
			{
				UserId = userId,
				PlanId = plan.StudyPlan.PlanId,
				Title = plan.StudyPlan.Title,
				Content = plan.StudyPlan.Content,
				Courses = plan.Courses,
				CreatorId = plan.StudyPlan.UserId,
				PlanStatus = plan.StudyPlan.Status,
				StudentCount = plan.StudyPlan.Student,
				CreateTime = plan.StudyPlan.CreateTime,
				StudyStatus = student.StudyStatus,
				Progress = student.Progress,
				LastStudyTime = student.LastStudyTime
			};
		}
	}
}
