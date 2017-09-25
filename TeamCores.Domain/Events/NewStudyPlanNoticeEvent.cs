using System;
using System.Collections.Generic;
using System.Linq;
using TeamCores.Common;
using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 新学习计划通知事件数据状态
	/// </summary>
	internal class NewStudyPlanNoticeEventState : DomainEventState
	{
		/// <summary>
		/// 学习计划
		/// </summary>
		public Data.Entity.StudyPlan Plan { get; set; }

		/// <summary>
		/// 学员
		/// </summary>
		public IEnumerable<long> StudentIds { get; set; }
	}

	/// <summary>
	/// 新学习计划通知事件
	/// </summary>
	internal class NewStudyPlanNoticeEvent : DomainEvent
	{
		public NewStudyPlanNoticeEvent(NewStudyPlanNoticeEventState state) : base(state)
		{
		}

		public override void Execute()
		{
			Validate();

			var state = State as NewStudyPlanNoticeEventState;

			if (state == null) return;

			if (state.StudentIds == null || state.StudentIds.Count() < 1) return;

			//遍历处理计划内的学员消息
			var list = new List<Data.Entity.Messages>();
			foreach (var userId in state.StudentIds)
			{
				list.Add(new Data.Entity.Messages
				{
					MessageId = IDProvider.NewId,
					Title = "你有新的学习计划，点击查看详情。",
					Content = $@"你已经被邀请加入学习计划《{state.Plan.Title}》，完成计划内的所有课程，还有更多练习（考）卷自测哦，<a href=""/UserStudyPlan/details/{state.Plan.PlanId}"">点击查看详细计划</a>。",
					CreateTime = DateTime.Now,
					Readed = false,
					ReadTime = null,
					Receiver = userId,
					Sender = state.Plan.UserId
				});
			}

			MessagesAccessor.AddRange(list);
		}
	}
}
