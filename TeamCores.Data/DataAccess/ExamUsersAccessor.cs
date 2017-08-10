using TeamCores.Data.Entity;

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
	}
}
