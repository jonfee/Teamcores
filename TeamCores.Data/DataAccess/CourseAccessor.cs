using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 课程存储服务
	/// </summary>
	public static class CourseAccessor
	{
		/// <summary>
		/// 插入课程
		/// </summary>
		/// <param name="course"></param>
		/// <returns></returns>
		public static bool Insert(Course course)
		{
			if (course == null) return false;

			using (var db = new DataContext())
			{
				db.Course.Add(course);

				return db.SaveChanges() > 0;
			}
		}
		
		/// <summary>
		/// 获取课程
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <returns></returns>
		public static Course Get(long courseId)
		{
			using (var db = new DataContext())
			{
				return db.Course.Find(courseId);
			}
		}
	}
}
