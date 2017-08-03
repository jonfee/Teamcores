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

		/// <summary>
		/// 获取学员包含指定课程的学习计划对应的所有课程集合
		/// </summary>
		/// <param name="studentId">学员用户ID</param>
		/// <param name="courseId">课程ID</param>
		/// <returns><seealso cref="Dictionary{TKey, TValue}"/>TKey:学习计划ID，TValue:学习计划下的课程ID集合</returns>
		public static Dictionary<long, long[]> GetStudyPlansCoursesFor(long studentId, long courseId)
		{
			Dictionary<long, long[]> dic = new Dictionary<long, long[]>();

			using (var db = new DataContext())
			{
				//用户的学习计划
				var userPlans = from p in db.UserStudyPlan
								where p.UserId == studentId
								select p.PlanId;

				//用户学习计划中包含了指定课程的计划
				var plans = (from p in db.StudyPlanCourse
							where p.CourseId == courseId && userPlans.Contains(p.PlanId)
							select p.PlanId).ToArray();

				//查出符合条件的计划下关联的课程
				dic = (from p in db.StudyPlanCourse
					   where plans.Contains(p.PlanId)
					   group p.CourseId by p.PlanId
					   into g
					   select g).ToDictionary(k => k.Key, v => v.ToArray());
			}

			return dic;
		}
	}
}
