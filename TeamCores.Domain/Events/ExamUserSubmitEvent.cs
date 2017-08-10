using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 用户提交考卷答案事件数据对象状态
	/// </summary>
	internal class ExamUserSubmitEventState : DomainEventState
	{
		/// <summary>
		/// 用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 考卷ID
		/// </summary>
		public long ExamId { get; set; }

		/// <summary>
		/// 用户提交的考卷ID
		/// </summary>
		public long UserExamId { get; set; }
	}

	/// <summary>
	/// 用户提交考卷答案事件
	/// </summary>
	internal class ExamUserSubmitEvent : DomainEvent
	{
		public ExamUserSubmitEvent(ExamUserSubmitEventState state) : base(state)
		{
		}

		public override void Execute()
		{
			Validate();

			var state = State as ExamUserSubmitEventState;

			ExamsAccessor.AddAnswerSubmitTimes(state.ExamId);
		}
	}
}
