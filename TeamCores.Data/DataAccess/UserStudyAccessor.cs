using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 用户学习情况统计数据服务 
	/// </summary>
	public static class UserStudyAccessor
	{
		/// <summary>
		/// 更新用户正在学习中的计划数量
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="count">正在学习中的计划数量</param>
		/// <returns></returns>
		public static bool UpdateStudyPlans(long userId, int count)
		{
			bool success = false;

			using (var db = new DataContext())
			{
				var item = db.UserStudy.Find(userId);

				if (item != null)
				{
					item.StudyPlans = count;

					db.UserStudy.Update(item);

					success = db.SaveChanges() > 0;
				}
			}

			return success;
		}

		/// <summary>
		/// 用户已学完课程新增
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static bool CoursesFinishedAdd(long userId)
		{
			bool success = false;

			using (var db = new DataContext())
			{
				var item = db.UserStudy.Find(userId);

				if (item != null)
				{
					item.ReadCount += 1;

					db.UserStudy.Update(item);

					success = db.SaveChanges() > 0;
				}
			}

			return success;
		}
	}
}
