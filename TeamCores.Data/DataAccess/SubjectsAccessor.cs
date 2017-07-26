using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 科目数据管理者
	/// </summary>
	public static class SubjectsAccessor
	{
		/// <summary>
		/// 检测科目名称是否存在
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool NameExists(string name)
		{
			using (var db = new DataContext())
			{
				return db.Subjects.Count(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) > 0;
			}
		}

		/// <summary>
		/// 添加科目
		/// </summary>
		/// <param name="subject"></param>
		/// <returns></returns>
		public static bool Insert(Subjects subject)
		{
			using (var db = new DataContext())
			{
				db.Subjects.Add(subject);

				return db.SaveChanges() > 0;
			}
		}
	}
}
