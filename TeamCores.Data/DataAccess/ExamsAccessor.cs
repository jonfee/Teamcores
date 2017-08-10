using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;
using TeamCores.Models;
using System.Linq;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 考卷的仓储服务
	/// </summary>
	public static class ExamsAccessor
	{
		/// <summary>
		/// 向数据库中插入一条考卷数据
		/// </summary>
		/// <param name="exams">考卷信息</param>
		/// <returns></returns>
		public static bool Insert(Exams exams)
		{
			if (exams == null) return false;

			using (var db = new DataContext())
			{
				db.Exams.Add(exams);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 获取考卷信息
		/// </summary>
		/// <param name="examsId">考卷ID</param>
		/// <returns></returns>
		public static Exams Get(long examsId)
		{
			using (var db = new DataContext())
			{
				return db.Exams.Find(examsId);
			}
		}

		/// <summary>
		/// 设置考卷状态
		/// </summary>
		/// <param name="examsId">考卷ID</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public static bool SetStatus(long examsId, int status)
		{
			bool success = false;

			using (var db = new DataContext())
			{
				var item = db.Exams.Find(examsId);

				if (item != null)
				{
					item.Status = status;

					success = db.SaveChanges() > 0;
				}
			}

			return success;
		}

		/// <summary>
		/// 更新考卷
		/// </summary>
		/// <param name="exams"></param>
		/// <returns></returns>
		public static bool Update(Exams exams)
		{
			if (exams == null) return false;

			bool success = false;

			using (var db = new DataContext())
			{
				db.Exams.Attach(exams);
				db.Entry(exams).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				success = db.SaveChanges() > 0;
			}

			return success;
		}

		/// <summary>
		/// 新增一次考卷使用次数
		/// </summary>
		/// <param name="examId">考卷ID</param>
		/// <returns></returns>
		public static bool AddUsedTimes(long examId)
		{
			bool success = false;

			using (var db = new DataContext())
			{
				var item = db.Exams.Find(examId);

				if (item != null)
				{
					item.UseCount += 1;

					success = db.SaveChanges() > 0;
				}
			}

			return success;
		}

		/// <summary>
		/// 新增一次考卷答题次数
		/// </summary>
		/// <param name="examId">考卷ID</param>
		/// <returns></returns>
		public static bool AddAnswerSubmitTimes(long examId)
		{
			bool success = false;

			using (var db = new DataContext())
			{
				var item = db.Exams.Find(examId);

				if (item != null)
				{
					item.Answers += 1;

					success = db.SaveChanges() > 0;
				}
			}

			return success;
		}

		/// <summary>
		/// 获取考卷列表
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="keyword"></param>
		/// <param name="courseId">课程ID,表示有关联此课程的考卷，为NULL时表示不限制</param>
		/// <param name="status">考卷状态，为NULL时表示不限制</param>
		/// <returns></returns>
		public static PagerModel<Exams> GetList(PagerModel<Exams> pager, string keyword, long? courseId = null, int? status = null)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.Exams
							select p;

				//课程
				if (courseId.HasValue)
				{
					query = from p in query
							where p.CourseIds.Contains(courseId.Value.ToString())
							select p;
				}

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
			}

			return pager;
		}
	}
}
