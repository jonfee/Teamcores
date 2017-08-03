using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 用户学习计划的学习进度更新事件数据状态
	/// </summary>
	internal class UserStudyProgressUpdateEventState : DomainEventState
	{
		/// <summary>
		/// 课程ID
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 学员ID
		/// </summary>
		public long StudentId { get; set; }
	}

	/// <summary>
	/// 用户学习计划的学习进度更新事件
	/// </summary>
	internal class UserStudyProgressUpdateEvent : DomainEvent
	{
		public UserStudyProgressUpdateEvent(UserStudyProgressUpdateEventState state) : base(state) { }

		public override void Execute()
		{
			var state = State as UserStudyProgressUpdateEventState;

			Validate();

			//找出学习计划中包含该课程的计划下的关联课程集合
			var planCourseDic = StudyPlanCourseAccessor.GetStudyPlansCoursesFor(state.StudentId, state.CourseId);

			//所有课程ID
			var courseIds = new Func<IEnumerable<long>>(() =>
			{
				List<long> tempIds = new List<long>();
				foreach (var ids in planCourseDic.Values)
				{
					tempIds.AddRange(ids);
				}
				return tempIds.Distinct();
			}).Invoke();

			//所有课程对应的章节集合
			var courseChapters = CourseAccessor.GetCourseChaptersFor(courseIds);

			//获取用户相关课程下学习过的章节ID集合
			var studiedChapterIds = StudyRecordAccessor.GetChapterIdsFor(state.StudentId, courseIds);

			//遍历计划，计算对应的计划学习进度

		}
	}
}
