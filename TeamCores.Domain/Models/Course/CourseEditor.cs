using System.Collections.Generic;
using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Events;

namespace TeamCores.Domain.Models.Course
{
	/// <summary>
	/// 课程编辑时验证错误结果枚举
	/// </summary>
	internal enum CourseEditFailureRule
	{
		/// <summary>
		/// 当前已经是启用状态
		/// </summary>
		[Description("当前已经是启用状态")]
		OBJECT_IS_NULL = 1,
		/// <summary>
		/// 当前状态不能设置为“启用”
		/// </summary>
		[Description("当前状态不能设置为“启用”")]
		STATUS_CANNOT_SET_TO_ENABLED,
		/// <summary>
		/// 当前状态不能设置为“禁用”
		/// </summary>
		[Description("当前状态不能设置为“禁用”")]
		STATUS_CANNOT_SET_TO_DISABLED,
		/// <summary>
		/// 所属科目不存在
		/// </summary>
		[Description("所属科目不存在")]
		SUBJECT_NOT_EXISTS,
		/// <summary>
		/// 课程标题不能为空
		/// </summary>
		[Description("课程标题不能为空")]
		TITLE_CANNOT_NULL_OR_EMPTY,
		/// <summary>
		/// 课程内容不能为空
		/// </summary>
		[Description("课程内容不能为空")]
		CONTENT_CANNOT_NULL_OR_EMPTY,
		/// <summary>
		/// 学习目标不能为空
		/// </summary>
		[Description("学习目标不能为空")]
		OBJECTIVE_CANNOT_NULL_OR_EMPTY,
		/// <summary>
		/// 不允许编辑
		/// </summary>
		[Description("不允许编辑")]
		CANNOT_MODIFY
	}

	/// <summary>
	/// 课程被修改后的状态
	/// </summary>
	internal class CourseModifiedState
	{
		/// <summary>
		/// 归属科目
		/// </summary>
		public long SubjectId { get; set; }

		/// <summary>
		/// 课程标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 课程封面图片
		/// </summary>
		public string Image { get; set; }

		/// <summary>
		/// 摘要
		/// </summary>
		public string Remarks { get; set; }

		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 学习目标
		/// </summary>
		public string Objective { get; set; }

		/// <summary>
		/// 课程状态
		/// </summary>
		public int Status { get; set; }
	}

	internal class CourseEditor : StudyProgressEntityBase<long, CourseEditFailureRule>
	{
		#region 属性

		/// <summary>
		/// 当前课程对象
		/// </summary>
		public readonly Data.Entity.Course Course;

		#endregion

		#region 构造实例

		public CourseEditor(Data.Entity.Course course)
		{
			if (course != null)
			{
				ID = course.CourseId;
				Course = course;
			}
		}

		public CourseEditor(long courseId)
		{
			ID = courseId;

			Course = CourseAccessor.Get(courseId);
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//操作的对象为NULL
			if (Course == null) AddBrokenRule(CourseEditFailureRule.OBJECT_IS_NULL);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 是否允许设置为“启用”
		/// </summary>
		/// <returns></returns>
		public bool CanSetToEnable()
		{
			return Course != null && Course.Status == (int)CourseStatus.DISABLED;
		}

		/// <summary>
		/// 是否允许设置为“禁用”
		/// </summary>
		/// <returns></returns>
		public bool CanSetToDisable()
		{
			return Course != null && Course.Status == (int)CourseStatus.ENABLED;
		}

		/// <summary>
		/// 是否允许删除
		/// </summary>
		/// <returns></returns>
		public bool CanDelete()
		{
			return false;
		}

		/// <summary>
		/// 是否允许修改
		/// </summary>
		/// <returns></returns>
		public bool CanModify()
		{
			return true;
		}

		/// <summary>
		/// 设置为“启用”状态
		/// </summary>
		/// <returns></returns>
		public bool SetEnable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetToEnable()) AddBrokenRule(CourseEditFailureRule.STATUS_CANNOT_SET_TO_ENABLED);
			});

			bool success= CourseAccessor.SetStatus(ID, (int)CourseStatus.ENABLED);

			if (success) ComputeStudyProgress(ID);

			return success;
		}

		/// <summary>
		/// 设置为“禁用”状态
		/// </summary>
		/// <returns></returns>
		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetToDisable()) AddBrokenRule(CourseEditFailureRule.STATUS_CANNOT_SET_TO_DISABLED);
			});

			bool success= CourseAccessor.SetStatus(ID, (int)CourseStatus.DISABLED);

			if (success) ComputeStudyProgress(ID);

			return success;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <returns></returns>
		public bool Delete()
		{
			return false;
		}

		/// <summary>
		/// 修改信息
		/// </summary>
		/// <param name="state">将要修改后的状态</param>
		/// <returns></returns>
		public bool ModifyTo(CourseModifiedState state)
		{
			if (state == null) return false;

			ThrowExceptionIfValidateFailure(() =>
			{
				//是否允许被编辑
				if (CanModify())
				{
					//课程标题为空时
					if (string.IsNullOrWhiteSpace(state.Title)) AddBrokenRule(CourseEditFailureRule.TITLE_CANNOT_NULL_OR_EMPTY);

					//课程内容为空时
					if (string.IsNullOrWhiteSpace(state.Content)) AddBrokenRule(CourseEditFailureRule.CONTENT_CANNOT_NULL_OR_EMPTY);

					//学习目标为空时
					if (string.IsNullOrWhiteSpace(state.Objective)) AddBrokenRule(CourseEditFailureRule.OBJECTIVE_CANNOT_NULL_OR_EMPTY);

					//所属科目不存在时
					if (!SubjectsAccessor.Exists(state.SubjectId)) AddBrokenRule(CourseEditFailureRule.SUBJECT_NOT_EXISTS);
				}
				else
				{
					AddBrokenRule(CourseEditFailureRule.CANNOT_MODIFY);
				}
			});

			bool success= CourseAccessor.Update(
				ID,
				state.SubjectId,
				state.Title,
				state.Image,
				state.Content,
				state.Remarks,
				state.Objective,
				state.Status);

			if (success && Course.Status != state.Status) ComputeStudyProgress(ID);

			return success;
		}

		/// <summary>
		/// 获取所有章节
		/// </summary>
		/// <returns></returns>
		public List<Data.Entity.Chapter> GetChapters()
		{
			return ChapterAccessor.GetList(ID);
		}

		/// <summary>
		/// 获取课程所在科目的名称
		/// </summary>
		/// <returns></returns>
		public string GetSubjectName()
		{
			return SubjectsAccessor.GetName(Course.SubjectId);
		}

		#endregion
	}
}
