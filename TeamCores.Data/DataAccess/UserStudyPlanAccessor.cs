using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Models;

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
		public static UserStudyPlan Get(long studyPlanId, long studentId)
		{
			using (var db = new DataContext())
			{
				return db.UserStudyPlan.FirstOrDefault(p => p.PlanId == studyPlanId && p.UserId == studentId);
			}
		}

		/// <summary>
		/// 分页获取用户学习计划列表信息
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="studentId">学员ID,为NULL时忽略</param>
		/// <param name="status">学习状态，为null时表示不限制</param>
		/// <returns></returns>
		public static PagerModel<UserStudyPlan> Get(PagerModel<UserStudyPlan> pager, long? studentId, int? status = null)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.UserStudyPlan
							select p;

				if (studentId.HasValue)
				{
					query = from p in query
							where p.UserId == studentId.Value
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
		/// 获取用户学习计划情况列表
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static List<UserStudyPlanProgressModel> GetPlansFor(long userId)
		{
			List<UserStudyPlanProgressModel> list = new List<UserStudyPlanProgressModel>();

			using (var db = new DataContext())
			{
				list = (from p in db.UserStudyPlan
						where p.UserId == userId
						select new UserStudyPlanProgressModel
						{
							PlanId = p.PlanId,
							StudentId = p.UserId,
							Progress = p.Progress,
							Status = p.Status
						}).ToList();
			}

			return list;
		}

		/// <summary>
		/// 获取用户指定状态下的计划数量
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="status">状态集合，为NULL表示全部</param>
		/// <returns></returns>
		public static int GetPlansCount(long userId, IEnumerable<int> status = null)
		{
			int count = 0;

			using (var db = new DataContext())
			{
				var query = from p in db.UserStudyPlan
							where p.UserId == userId
							select p;

				if (status != null && status.Count() > 0)
				{
					var st = status.ToList();

					query = query.Where(p => st.Contains(p.Status));
				}

				count = query.Count();
			}

			return count;
		}

		/// <summary>
		/// 获取学习计划下的学员ID集合
		/// </summary>
		/// <param name="plans"></param>
		/// <returns></returns>
		public static Dictionary<long, long[]> GetStudentIdsFor(IEnumerable<long> plans)
		{
			if (plans == null || !plans.Any()) return null;

			using (var db = new DataContext())
			{
				return (from p in db.UserStudyPlan
						where plans.Contains(p.PlanId)
						group p.PlanId by p.UserId into g
						select g).ToDictionary(k => k.Key, v => v.ToArray());
			}
		}

		/// <summary>
		/// 更新学习计划进度
		/// </summary>
		/// <param name="planProgressList">计划对应的进度</param>
		/// <returns></returns>
		public static bool UpdateProgress(List<UserStudyPlanProgressModel> planProgressList)
		{
			if (planProgressList == null || planProgressList.Count() < 1) return false;

			bool success = false;

			using (var db = new DataContext())
			{
				foreach (var item in planProgressList)
				{
					var plan = new UserStudyPlan
					{
						PlanId = item.PlanId,
						UserId = item.StudentId,
						Progress = item.Progress,
						Status = item.Status,
						UpdateTime = DateTime.Now
					};

					db.UserStudyPlan.Attach(plan);
					db.Entry(plan).Property(p => p.Progress).IsModified = true;
					db.Entry(plan).Property(p => p.UpdateTime).IsModified = true;
					db.Entry(plan).Property(p => p.Status).IsModified = true;
				}

				success = db.SaveChanges() > 0;
			}

			return success;
		}
	}
}
