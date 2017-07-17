using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Models
{
	/// <summary>
	/// 用户集合管理信息验证失败规则
	/// </summary>
	public enum UserManageFailureRules
	{

	}

	public class UserManage : EntityBase<long, UserManageFailureRules>
	{
		/// <summary>
		/// 用户集合
		/// </summary>
		private List<UserAccount> Users;

		protected override void Validate()
		{
			throw new NotImplementedException();
		}
	}
}
