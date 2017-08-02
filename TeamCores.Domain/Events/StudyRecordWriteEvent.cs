using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 用户学习记录写入事件数据状态
	/// </summary>
	internal class StudyRecordWriteEventState
	{
		/// <summary>
		/// 学员ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 课程ID
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 课程章节ID
		/// </summary>
		public long ChapterId { get; set; }
	}

	/// <summary>
	/// 用户学习记录写入事件
	/// </summary>
	internal class StudyRecordWriteEvent : DomainEvent
	{
		StudyRecordWriteEventState state;

		public StudyRecordWriteEvent(StudyRecordWriteEventState state)
		{
			this.state = state;
		}

		public override void Execute()
		{
			throw new NotImplementedException();
		}
	}
}
