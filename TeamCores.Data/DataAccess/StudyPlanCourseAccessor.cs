using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;
using System.Linq;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 学习计划关联的课程仓储服务
	/// </summary>
	public static class StudyPlanCourseAccessor
	{
		/// <summary>
		/// 获取学习计划下的课程信息集合
		/// </summary>
		/// <param name="studyPlanId">学习计划ID</param>
		/// <returns></returns>
		public static List<StudyPlanCourse> GetCourseList(long studyPlanId)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.StudyPlanCourse
							where p.PlanId == studyPlanId
							select p;

				return query.ToList();
			}
		}
	}
}
