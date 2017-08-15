using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 权限仓储服务
	/// </summary>
	public static class PermissionAccessor
	{
		/// <summary>
		///  获取所有权限
		/// </summary>
		/// <returns></returns>
		public static List<Permission> GetAll()
		{
			using (var db = new DataContext())
			{
				return (from p in db.Permission
						select p).ToList();
			}
		}

		/// <summary>
		/// 插入多条权限数据到数据库，返回影响的行数
		/// </summary>
		/// <param name="permissions"></param>
		/// <returns></returns>
		public static int Insert(IEnumerable<Permission> permissions)
		{
			using (var db = new DataContext())
			{
				db.Permission.AddRange(permissions);

				return db.SaveChanges();
			}
		}
	}
}
