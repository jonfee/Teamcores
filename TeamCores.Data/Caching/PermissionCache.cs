using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Data.Init;

namespace TeamCores.Data.Caching
{
	/// <summary>
	/// 权限缓存
	/// </summary>
	public sealed class PermissionCache : DataCache<Permission>
	{
		private static PermissionCache _instance;

		public static PermissionCache Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (my_locker)
					{
						if (_instance == null)
						{
							_instance = new PermissionCache();
						}
					}
				}

				return _instance;
			}
		}

		private PermissionCache() { }

		protected override Dictionary<object, Permission> GetInitData()
		{
			var list = PermissionAccessor.GetAll();

			if (list == null || list.Count < 1)
			{
				var init = new PermissionInit();

				init.Save();

				list = init.PermissionList;
			}

			var dic = new Dictionary<object, Permission>();

			if (list != null)
			{
				foreach (var item in list)
				{
					dic.Add(item.Id, item);
				}
			}

			return dic;
		}

		/// <summary>
		/// 获取权限编号序列组
		/// </summary>
		/// <param name="separator">用指定的分隔符隔开，如分隔符为“,”号，则效果如：A01,A02</param>
		/// <returns></returns>
		public string GetCodes(string separator = "")
		{
			var list = PermissionAccessor.GetAll();

			if (list == null || list.Count < 1) return string.Empty;

			var ids = list.Select(p => p.Code);

			return string.Join(separator, ids);
		}

		/// <summary>
		/// 获取权限编号数组
		/// </summary>
		/// <returns></returns>
		public string[] GetCodeArray()
		{
			var list = PermissionAccessor.GetAll();

			if (list == null || list.Count < 1) return null;

			return list.Select(p => p.Code).ToArray();
		}
	}
}
