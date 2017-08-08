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
		/// 用户学习计划不存在
		/// </summary>
		USERSTUDYPLAN_NOT_EXISTS = 1
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

		private Data.Entity.UserStudyPlan plan;
		/// <summary>
		/// 用户的学习计划
		/// </summary>
		public Data.Entity.UserStudyPlan Plan
		{
			get
			{
				if (plan == null)
				{
					plan = UserStudyPlanAccessor.Get(ID, UserId);
				}

				return plan;
			}
		}

		#endregion

		#region 构造函数

		/// <summary>
		/// 实例化<see cref="UserStudyPlanManage"/>对象
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="planId"></param>
		public UserStudyPlanManage(long userId,long planId)
		{
			ID = planId;
			UserId = userId;
		}

		#endregion

		#region 验证
		protected override void Validate()
		{
			if (Plan == null) AddBrokenRule(UserStudyPlanManageFailureRule.USERSTUDYPLAN_NOT_EXISTS);
		}
		#endregion

		#region 操作方法

		

		#endregion
	}
}
