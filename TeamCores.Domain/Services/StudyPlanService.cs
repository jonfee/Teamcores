using System;
using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Models.StudyPlan;
using TeamCores.Domain.Services.Response;
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
		public PagerModel<StudyPlanResponse> Search(int pageSize, int pageIndex, string keyword, int? status)
		{
			StudyPlanSearch search = new StudyPlanSearch(pageIndex, pageSize, keyword, status);

			var result = search.Search();

			var userIds = result.Table.Select(p => p.UserId);

			var names = UsersAccessor.GetUsernames(userIds);

			List<StudyPlanResponse> list = new List<StudyPlanResponse>();

			foreach (var item in result.Table)
			{
				var temp = new StudyPlanResponse(item);

				string userName = names.ContainsKey(item.UserId) ? names[item.UserId] : string.Empty;

				temp.Creator = userName;

				list.Add(temp);
			}

			return new PagerModel<StudyPlanResponse>
			{
				Count = result.Count,
				Index = result.Index,
				Size = result.Size,
				Table = list
			};
		}

		/// <summary>
		/// 设置状态为“启用”
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		public bool SetEnable(long planId)
		{
			var plan = new StudyPlanManage(planId);

			return plan.SetEnable();
		}

		/// <summary>
		/// 设置状态为“禁用”
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		public bool SetDisable(long planId)
		{
			var plan = new StudyPlanManage(planId);

			return plan.SetDisable();
		}

		/// <summary>
		/// 获取学习计划的详细信息
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		public StudyPlanDetails GetStudyPlanDetails(long planId)
		{
			var plan = new StudyPlanManage(planId);

			var data = plan.ConvertToStudyPlanDetails();

			return data;
		}

		/// <summary>
		/// 获取指定学员对学习计划的学习情况
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <param name="userId">学员ID</param>
		/// <returns></returns>
		public StudentPlanStudingDetails GetyPlanStudingDetails(long planId, long userId)
		{
			var plan = new StudyPlanManage(planId);

			var data = plan.GetStudentStudingDetails(userId);

			return data;
		}
	}
}
