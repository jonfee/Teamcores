using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 课程是否学习完成检测事件数据状态
	/// </summary>
	internal class CourseStudyFinishedCheckEventState : DomainEventState
	{
		/// <summary>
		/// 学员ID
		/// </summary>
		public long StudentId { get; set; }

		/// <summary>
		/// 课程ID
		/// </summary>
		public long CoureseId { get; set; }
	}

	/// <summary>
	/// 课程是否学习完成检测事件
	/// </summary>
	internal class CourseStudyFinishedCheckEvent : DomainEvent
	{
		/// <summary>
		/// 初始化一个<see cref="CourseStudyFinishedCheckEvent"/>对象实例
		/// </summary>
		/// <param name="state"></param>
		public CourseStudyFinishedCheckEvent(CourseStudyFinishedCheckEventState state) : base(state)
		{
		}

		/// <summary>
		/// 如果检测到该课程已学习完成，将更新用户课程完成数量
		/// </summary>
		public override void Execute()
		{
			Validate();

			var state = State as CourseStudyFinishedCheckEventState;

			//获取课程下的所有章节状态信息
			var courseChapters = ChapterAccessor.GetChaptersAllFor(state.CoureseId);
			//有效的课程章节
			var enableChapters = courseChapters.Where(p => p.Status == (int)ChapterStatus.ENABLED);
			//有效的课程章节ID集合
			var enabledChapterIds = enableChapters.Select(p => p.ChapterId);
			//有效的课程章节数
			var enabledCount = enableChapters != null ? enableChapters.Count() : 0;

			//获取用户当前课程学习过的章节信息
			var studiedChapters = StudyRecordAccessor.GetChapterIdsFor(state.StudentId, state.CoureseId);
			//已学习过的课程章节数
			var studiedCount = studiedChapters.Count(p => enabledChapterIds.Contains(p));
			
			//如果：课程中开启的章节数均已学习完成，则更新用户课程完成数
			if (enabledCount == studiedCount)
			{
				UserStudyAccessor.CoursesFinishedAdd(state.StudentId);
			}
		}
	}
}
