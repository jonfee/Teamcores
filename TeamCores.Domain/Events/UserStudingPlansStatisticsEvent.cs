using System;
using System.Collections.Generic;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Models.UserStuding;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 用户正在进行中的学习计划统计事件数据状态
	/// </summary>
	internal class UserStudingPlansStatisticsEventState : DomainEventState
	{
		/// <summary>
		/// 学员ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 学习计划
		/// </summary>
		public long PlanId { get; set; }
	}

	/// <summary>
	/// 用户正在进行中的学习计划统计事件
	/// </summary>
	internal class UserStudingPlansStatisticsEvent : DomainEvent
	{
		public UserStudingPlansStatisticsEvent(UserStudingPlansStatisticsEventState state) : base(state) { }

		public override void Execute()
		{
			Validate();

			var state = State as UserStudingPlansStatisticsEventState;

			//学员学习计划管理
			var userPlanManager = new UserStudyPlanManage(state.UserId);

			//学习中的计划状态，指：未开始，学习中的状态
			var status = ((IList<int>)Enum.GetValues(typeof(UserStudyPlanStatus)));
			//排除已学习完成的
			status.Remove((int)UserStudyPlanStatus.COMPLETE);

			int count = userPlanManager.GetPlansCount(status);

			//更新仓储数据
			UserStudyAccessor.UpdateStudyPlans(state.UserId, count);
		}
	}
}
