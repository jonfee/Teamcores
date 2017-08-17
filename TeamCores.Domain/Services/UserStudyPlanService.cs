﻿using System;
using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Models.StudyPlan;
using TeamCores.Domain.Models.UserStuding;
using TeamCores.Domain.Services.Response;
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
		/// <param name="studentId">学员ID</param>
		/// <param name="studyStatus">学习状态，为NULL时忽略</param>
		/// <returns></returns>
		public PagerModel<UserStudyPlanSearchResultItem> Search(int pageSize, int pageIndex, long? studentId, int? studyStatus)
		{
			UserStudyPlanSearch search = new UserStudyPlanSearch(pageIndex, pageSize, studentId, studyStatus);

			var searchPager = search.Search();

			var data = TransferData(searchPager.Table);

			if (data.Count < 1) data.Add(new UserStudyPlanSearchResultItem
			{
				Content = "",
				CreateTime = DateTime.Now,
				Creator = "jonfee",
				PlanId = 12312312,
				StudyStatus = 1,
				StudentCount = 10,
				Title = "我是测试的学习计划",
				UserId = 1231,
				CreatorId = 111,
				LastStudyTime = null,
				PlanStatus = 1,
				Progress = 0.5f
			});

			return new PagerModel<UserStudyPlanSearchResultItem>
			{
				Index = searchPager.Index,
				Size = searchPager.Size,
				Count = searchPager.Count,
				Table = data
			};
		}

		/// <summary>
		/// 获取用户学习计划执行详细
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="planId">计划ID</param>
		/// <returns></returns>
		public UserStudyPlanDetails GetPlanDetails(long userId, long planId)
		{
			//学习计划
			var plan = new StudyPlanManage(planId);

			//指定学习对该计划的实施信息
			var userPlan = new UserStudyPlanManage(userId, planId);

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
				StudyStatus = userPlan.Plan.Status,
				Progress = userPlan.Plan.Progress,
				LastStudyTime = userPlan.Plan.UpdateTime
			};
		}

		/// <summary>
		/// 转换数据
		/// </summary>
		/// <param name="userPlans">用户学习计划情况集合</param>
		/// <returns></returns>
		private List<UserStudyPlanSearchResultItem> TransferData(IEnumerable<Data.Entity.UserStudyPlan> userPlans)
		{
			if (userPlans == null || userPlans.Count() < 1) return new List<UserStudyPlanSearchResultItem>();

			//用户学习计划ID集合
			var planIds = userPlans.Select(p => p.PlanId).ToArray();

			//获取所有的学习计划
			var plans = StudyPlanAccessor.GetList(planIds);

			if (plans == null || plans.Count() < 1) return null;

			var list = new List<UserStudyPlanSearchResultItem>();

			foreach (var plan in plans)
			{
				//用户学习计划执行情况
				var userPlan = userPlans.FirstOrDefault(p => p.PlanId == plan.PlanId);

				list.Add(new UserStudyPlanSearchResultItem
				{
					UserId = userPlan.UserId,
					PlanId = plan.PlanId,
					Title = plan.Title,
					Content = plan.Content,
					CreatorId = plan.UserId,
					StudentCount = plan.Student,
					PlanStatus = plan.Status,
					CreateTime = plan.CreateTime,
					StudyStatus = userPlan.Status,
					Progress = userPlan.Progress,
					LastStudyTime = userPlan.UpdateTime
				});
			}

			return list;
		}
	}
}
