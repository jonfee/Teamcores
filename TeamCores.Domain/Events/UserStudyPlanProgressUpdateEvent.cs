using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 用户学习计划的学习进度更新事件数据状态
	/// </summary>
	internal class UserStudyPlanProgressUpdateEventState
	{
		/// <summary>
		/// 课程章节ID
		/// </summary>
		public long ChapterId { get; set; }

		/// <summary>
		/// 课程ID
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 学员ID
		/// </summary>
		public long UserId { get; set; }
	}

	/// <summary>
	/// 用户学习计划的学习进度更新事件
	/// </summary>
	internal class UserStudyPlanProgressUpdateEvent : DomainEvent
	{
		private UserStudyPlanProgressUpdateEventState state;

		public UserStudyPlanProgressUpdateEvent(UserStudyPlanProgressUpdateEventState state)
		{
			this.state = state;
		}

		public override void Execute()
		{
			throw new NotImplementedException();
		}
	}
}
