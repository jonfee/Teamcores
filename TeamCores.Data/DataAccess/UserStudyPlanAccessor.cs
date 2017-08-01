using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 学员学习计划
	/// </summary>
	public static class UserStudyPlanAccessor
	{
		/// <summary>
		/// 获取学习计划的学员ID集合
		/// </summary>
		/// <param name="studyPlanId">学习计划ID</param>
		/// <returns></returns>
		public static long[] GetStudents(long studyPlanId)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.UserStudyPlan
							where p.PlanId == studyPlanId
							select p.UserId;

				return query.ToArray();
			}
		}
		/// <summary>
		/// 获取学习计划的学员计划集合
		/// </summary>
		/// <param name="studyPlanId">学习计划ID</param>
		/// <returns></returns>
		public static List<UserStudyPlan> GetStudentStudyPlans(long studyPlanId)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.UserStudyPlan
							where p.PlanId == studyPlanId
							select p;

				return query.ToList();
			}
		}

		/// <summary>
		/// 获取学员学习计划
		/// </summary>
		/// <param name="studyPlanId">学习计划ID</param>
		/// <param name="studentId">学员ID</param>
		/// <returns></returns>
		public static UserStudyPlan GetUserStudyPlan(long studyPlanId, long studentId)
		{
			using (var db = new DataContext())
			{
				return db.UserStudyPlan.FirstOrDefault(p => p.PlanId == studyPlanId && p.UserId == studentId);
			}
		}
	}
}
