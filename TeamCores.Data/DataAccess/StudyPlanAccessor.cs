using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 学习计划仓储服务
	/// </summary>
	public static class StudyPlanAccessor
	{
		/// <summary>
		/// 检测学习计划标题是否已存在
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public static bool Exists(string title)
		{
			if (string.IsNullOrWhiteSpace(title)) return false;

			using (var db = new DataContext())
			{
				return db.StudyPlan.Count(p => string.Compare(title, p.Title, true) == 0) > 0;
			}
		}
	}
}
