using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 用户考试信息仓储服务
	/// </summary>
	public static class ExamUsersAccessor
	{
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
	}
}
