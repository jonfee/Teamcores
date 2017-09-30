using System.Collections.Generic;
using TeamCores.Data.Entity;
using TeamCores.Models;
using System.Linq;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 用户考试信息仓储服务
	/// </summary>
	public static class ExamUsersAccessor
	{
		/// <summary>
		/// 插入一条用户考卷信息
		/// </summary>
		/// <param name="exam"></param>
		/// <returns></returns>
		public static bool Insert(ExamUsers exam)
		{
			if (exam == null) return false;

			bool success = false;

			using (var db = new DataContext())
			{
				db.ExamUsers.Add(exam);

				success = db.SaveChanges() > 0;
			}

			return success;
		}

		/// <summary>
		/// 获取用户考卷信息
		/// </summary>
		/// <param name="userExamId">用户考卷ID</param>
		/// <returns></returns>
		public static ExamUsers Get(long userExamId)
		{
			using (var db = new DataContext())
			{
				return db.ExamUsers.Find(userExamId);
			}
		}

		/// <summary>
		/// 更新用户考卷信息
		/// </summary>
		/// <param name="exam">用户考卷</param>
		/// <returns></returns>
		public static bool Update(ExamUsers exam)
		{
			if (exam == null) return false;

			bool success = false;

			using (var db = new DataContext())
			{
				db.ExamUsers.Attach(exam);
				db.Entry(exam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				success = db.SaveChanges() > 0;
			}

			return success;
		}

		/// <summary>
		/// 移除用户考卷信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool Remove(long id)
		{
			using (var db = new DataContext())
			{
				var item = db.ExamUsers.Find(id);

				if (item != null)
				{
					db.ExamUsers.Remove(item);

					return db.SaveChanges() == 1;
				}

				return true;
			}
		}

		/// <summary>
		/// 移除用户考卷信息
		/// </summary>
		/// <param name="exam"></param>
		/// <returns></returns>
		public static bool Remove(ExamUsers exam)
		{
			using (var db = new DataContext())
			{
				db.ExamUsers.Attach(exam);

				db.Entry(exam).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

				return db.SaveChanges() == 1;
			}
		}

		/// <summary>
		/// 分页获取用户考卷集合
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="userId">考生ID</param>
		/// <param name="examId">考卷模板ID</param>
		/// <param name="markingStatus">考卷阅卷状态</param>
		/// <returns></returns>
		public static PagerModel<ExamUsers> GetList(PagerModel<ExamUsers> pager, long? userId, long? examId = null, int? markingStatus = null)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.ExamUsers
							select p;

				//考生
				if (userId.HasValue)
				{
					query = from p in query
							where p.UserId == userId
							select p;
				}

				//考卷模板
				if (examId.HasValue)
				{
					query = from p in query
							where p.ExamId == examId
							select p;
				}

				//指定阅卷状态
				if (markingStatus.HasValue)
				{
					query = from p in query
							where p.MarkingStatus == markingStatus
							select p;
				}

				pager.Count = query.Count();
				pager.Table = query.OrderByDescending(p => p.CreateTime).Skip((pager.Index - 1) * pager.Size).Take(pager.Size).ToList();
			}

			return pager;
		}

		/// <summary>
		/// 获取用户考卷信息列表
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static List<UserExamStatusModel> GetListFor(long userId)
		{
			var list = new List<UserExamStatusModel>();

			using (var db = new DataContext())
			{
				list = (from p in db.ExamUsers
						where p.UserId == userId
						select new UserExamStatusModel
						{
							Id = p.Id,
							UserId = p.UserId,
							CreateTime = p.CreateTime,
							MarkingStatus = p.MarkingStatus,
							PostTime = p.PostTime,
							Times = p.Times
						}).ToList();

				return list;
			}
		}
	}
}
