using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using System.Linq;
using System.Collections;

namespace TeamCores.Domain.Models.UserStuding
{
	/// <summary>
	/// 用户学习计划管理业务验证失败结果枚举
	/// </summary>
	internal enum UserStudyPlanManageFailureRule
	{
		/// <summary>
		/// 学员不存在
		/// </summary>
		[Description("学员不存在")]
		STUDENT_NOT_EXISTS = 1
	}

	/// <summary>
	/// 用户学习计划管理业务领域模型
	/// </summary>
	internal class UserStudyPlanManage : EntityBase<long, UserStudyPlanManageFailureRule>
	{
		#region 属性

		/// <summary>
		/// 学员用户ID
		/// </summary>
		public long UserId { get; set; }

		#endregion

		#region 构造函数

		/// <summary>
		/// 实例化<see cref="UserStudyPlanManage"/>对象
		/// </summary>
		/// <param name="userId"></param>
		public UserStudyPlanManage(long userId)
		{
			ID = UserId;
			UserId = userId;
		}

		#endregion

		#region 验证
		protected override void Validate()
		{
			if (!UsersAccessor.Exists(UserId)) AddBrokenRule(UserStudyPlanManageFailureRule.STUDENT_NOT_EXISTS);
		}
		#endregion

		#region 操作方法

		/// <summary>
		/// 获取名下学习计划数
		/// </summary>
		/// <param name="status">指定的计划状态，为NULL时表示全部</param>
		/// <returns></returns>
		public int GetPlansCount(IEnumerable<int> status)
		{
			return UserStudyPlanAccessor.GetPlansCount(UserId, status);
		}

		#endregion
	}
}
