﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 学习计划仓储服务
	/// </summary>
	public static class StudyPlanAccessor
	{
		/// <summary>
		/// 检测学习计划标题是否已存在
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public static bool Exists(string title)
		{
			if (string.IsNullOrWhiteSpace(title)) return false;

			using (var db = new DataContext())
			{
				return db.StudyPlan.Count(p => string.Compare(title, p.Title, true) == 0) > 0;
			}
		}

		/// <summary>
		/// 插入新计划
		/// </summary>
		/// <param name="plan">学习计划</param>
		/// <param name="courses">学习计划关联的课程</param>
		/// <param name="usersPlan">用户的学习计划</param>
		/// <returns></returns>
		public static bool Insert(StudyPlan plan, IEnumerable<StudyPlanCourse> courses, IEnumerable<UserStudyPlan> usersPlan)
		{
			if (plan == null) return false;

			if (courses == null || courses.Count() < 1) return false;

			if (usersPlan == null || usersPlan.Count() < 1) return false;

			using (var db = new DataContext())
			{
				db.StudyPlan.Add(plan);
				db.StudyPlanCourse.AddRange(courses);
				db.UserStudyPlan.AddRange(usersPlan);

				return db.SaveChanges() > 0;
			}
		}
	}
}
