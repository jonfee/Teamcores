using System;
using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.Caching;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Models.Permission
{
	internal enum PermissionManageFailureRule
	{

	}

	/// <summary>
	/// 权限点管理业务领域
	/// </summary>
	internal class PermissionManage : EntityBase<long, PermissionManage>
	{
		#region 属性

		private List<Data.Entity.Permission> _allPermissions;
		/// <summary>
		/// 所有权限
		/// </summary>
		public List<Data.Entity.Permission> AllPermissions
		{
			get
			{
				if (_allPermissions == null)
				{
					_allPermissions = PermissionCache.Instance.GetAll();
				}

				return _allPermissions;
			}
		}

		#endregion

		#region 验证
		protected override void Validate()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region 操作方法

		/// <summary>
		/// 获取以模块为分组的权限点集合
		/// </summary>
		/// <returns></returns>
		public List<PermissionResponse> GroupByModule()
		{
			return (from p in AllPermissions
					group new PermissionResponseItem
					{
						Code = p.Code,
						Name = p.Name
					} by p.Module into g
					select new PermissionResponse
					{
						Module=g.Key,
						Items=g.ToList()
					}).ToList();
		}

		#endregion
	}
}
