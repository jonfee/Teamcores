using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;

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
	}
}
