using TeamCores.Domain.Events;

namespace TeamCores.Domain.Models
{
	/// <summary>
	/// 影响用户学习计划进度的领域抽象基类
	/// </summary>
	/// <typeparam name="TID">领域对象ID数据类型</typeparam>
	/// <typeparam name="TRule">验证失败的规则结果枚举类型</typeparam>
	internal abstract class StudyProgressEntityBase<TID, TRule> : EntityBase<TID, TRule>
	{
		/// <summary>
		/// 立即执行，计算由指定课程影响的学习计划进度
		/// </summary>
		/// <param name="courseId">课程ID</param>
		protected void ComputeStudyProgress(long courseId)
		{
			var @event = new UserStudyProgressUpdateEvent(new UserStudyProgressUpdateEventState
			{
				CourseId = courseId
			});

			@event.Execute();
		}
	}
}
