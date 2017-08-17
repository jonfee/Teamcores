using System.Collections.Generic;
using TeamCores.Domain.Models.Permission;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 权限点业务服务
	/// </summary>
	public class PermissionService
	{
		/// <summary>
		/// 获取所有权限点
		/// </summary>
		/// <returns></returns>
		public List<Data.Entity.Permission> GetAllPermissions()
		{
			var manage = new PermissionManage();

			return manage.AllPermissions;
		}

		/// <summary>
		/// 获取以模块为分组的权限点集合
		/// </summary>
		/// <returns></returns>
		public List<PermissionResponse> GetPermissionsGroupByModule()
		{
			var manage = new PermissionManage();

			return manage.GroupByModule();
		}
	}
}
