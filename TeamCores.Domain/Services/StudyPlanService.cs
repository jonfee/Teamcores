﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Models.StudyPlan;
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
	}
}
