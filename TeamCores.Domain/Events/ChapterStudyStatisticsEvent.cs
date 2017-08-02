using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 课程章节学习统计事件数据状态
	/// </summary>
	internal class ChapterStudyStatisticsEventState
	{
		/// <summary>
		/// 正在学习的章节ID
		/// </summary>
		public long ChapterId { get; set; }

		/// <summary>
		/// 正在学习的用户ID
		/// </summary>
		public long UserId { get; set; }
	}

	/// <summary>
	/// 课程章节学习统计事件
	/// </summary>
	internal class ChapterStudyStatisticsEvent : DomainEvent
	{
		ChapterStudyStatisticsEventState state;

		public ChapterStudyStatisticsEvent(ChapterStudyStatisticsEventState state)
		{
			this.state = state;
		}

		public override void Execute()
		{
			throw new NotImplementedException();
		}
	}
}
