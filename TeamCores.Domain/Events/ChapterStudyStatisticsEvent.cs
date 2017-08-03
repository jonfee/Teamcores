using TeamCores.Data.DataAccess;
using TeamCores.Domain.Models.Chapter;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 课程章节学习统计事件数据状态
	/// </summary>
	internal class ChapterStudyStatisticsEventState : DomainEventState
	{
		/// <summary>
		/// 正在学习的章节
		/// </summary>
		public Data.Entity.Chapter Chapter { get; set; }

		/// <summary>
		/// 正在学习的学员用户ID
		/// </summary>
		public long StudentId { get; set; }
	}

	/// <summary>
	/// 课程章节学习统计事件
	/// </summary>
	internal class ChapterStudyStatisticsEvent : DomainEvent
	{
		public ChapterStudyStatisticsEvent(ChapterStudyStatisticsEventState state) : base(state) { }

		public override void Execute()
		{
			var state = State as ChapterStudyStatisticsEventState;

			Validate(() =>
			{
				if (state.Chapter == null) Throw("事件依赖的课程章节对象为NULL。");
			});

			if (state != null)
			{
				//获取章节的所有父章节
				var chapterIds = ChapterAccessor.GetParentsIds(state.Chapter.ChapterId);
				//包含自身
				chapterIds.Add(state.Chapter.ChapterId);

				ChapterAccessor.AddOnceTimesForStudy(chapterIds);
			}
		}
	}
}
