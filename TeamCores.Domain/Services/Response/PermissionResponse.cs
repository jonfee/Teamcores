using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Services.Response
{
	public class PermissionResponseItem
	{
		/// <summary>
		/// 权限编号
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 权限名称
		/// </summary>
		public string Name { get; set; }
	}

	public class PermissionResponse
	{
		public string Module { get; set; }

		public List<PermissionResponseItem> Items { get; set; }
	}
}
