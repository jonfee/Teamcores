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
	}
}
