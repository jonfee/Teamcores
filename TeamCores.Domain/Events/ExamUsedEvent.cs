using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 考卷模板使用事件数据状态
	/// </summary>
	internal class ExamUsedEventState : DomainEventState
	{
		/// <summary>
		/// 考卷模板ID
		/// </summary>
		public long ExamId { get; set; }
	}

	/// <summary>
	/// 考卷模板使用事件
	/// </summary>
	internal class ExamUsedEvent : DomainEvent
	{
		public ExamUsedEvent(ExamUsedEventState state) : base(state)
		{
		}

		public override void Execute()
		{
			Validate();

			var state = State as ExamUsedEventState;

			ExamsAccessor.AddUsedTimes(state.ExamId);
		}
	}
}
