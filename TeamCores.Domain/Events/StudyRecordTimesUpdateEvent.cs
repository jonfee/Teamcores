using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 学习记录学习次数更新事件数据状态
	/// </summary>
	internal class StudyRecordTimesUpdateEventState : DomainEventState
	{
		/// <summary>
		/// 学员用户ID
		/// </summary>
		public long StudentId { get; set; }

		/// <summary>
		/// 学习的课程章节ID
		/// </summary>
		public long ChapterId { get; set; }

		/// <summary>
		/// 是否包含自身更新
		/// </summary>
		public bool IncludeMySelf { get; set; }
	}

	/// <summary>
	/// 学习记录学习次数更新事件
	/// </summary>
	internal class StudyRecordTimesUpdateEvent : DomainEvent
	{
		/// <summary>
		/// 实例化<see cref="StudyRecordTimesUpdateEvent"/>对象实例
		/// </summary>
		/// <param name="state"></param>
		public StudyRecordTimesUpdateEvent(StudyRecordTimesUpdateEventState state) : base(state)
		{
		}

		public override void Execute()
		{
			var state = State as StudyRecordTimesUpdateEventState;
			
			Validate();

			//获取章节的所有父级章节ID
			var chapterIds = ChapterAccessor.GetParentsIds(state.ChapterId);

			if (state.IncludeMySelf)
			{
				chapterIds.Add(state.ChapterId);
			}

			StudyRecordAccessor.AddOnceTimesForStudy(state.StudentId, chapterIds);
		}
	}
}
