﻿using System.Collections.Generic;
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
	}
}
