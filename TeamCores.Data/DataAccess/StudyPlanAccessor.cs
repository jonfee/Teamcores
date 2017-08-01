using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Data.Entity;
using TeamCores.Models;

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
		/// 获取学习计划
		/// </summary>
		/// <param name="planId"></param>
		/// <returns></returns>
		public static StudyPlan Get(long planId)
		{
			using (var db = new DataContext())
			{
				return db.StudyPlan.Find(planId);
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

		/// <summary>
		/// 分页获取学习计划列表信息
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="keyword"></param>
		/// <param name="status">状态，为null时表示不限制</param>
		/// <returns></returns>
		public static PagerModel<StudyPlan> Get(PagerModel<StudyPlan> pager, string keyword, int? status = null)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.StudyPlan
							select p;

				//根据关键词查询
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					query = from p in query
							where p.Title.Contains(keyword)
							select p;
				}

				//指定状态
				if (status.HasValue)
				{
					query = from p in query
							where p.Status.Equals(status.Value)
							select p;
				}

				pager.Count = query.Count();
				pager.Table = query.OrderByDescending(p => p.CreateTime).Skip((pager.Index - 1) * pager.Size).Take(pager.Size).ToList();
				return pager;
			}
		}

		/// <summary>
		/// 设置学习计划状态
		/// </summary>
		/// <param name="planId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public static bool SetStatus(long planId, int status)
		{
			using (var db = new DataContext())
			{
				var item = db.StudyPlan.Find(planId);

				item.Status = status;

				db.StudyPlan.Update(item);

				return db.SaveChanges() > 0;
			}
		}
	}
}
