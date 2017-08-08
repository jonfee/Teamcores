using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Events;
using TeamCores.Domain.Utility;

namespace TeamCores.Domain.Models.Chapter
{
	/// <summary>
	/// 课程章节编辑验证错误结果枚举
	/// </summary>
	internal enum ChapterManageFailureRule
	{
		/// <summary>
		/// 当前操作的课程章节不存在
		/// </summary>
		[Description("当前操作的课程章节不存在")]
		CHAPTER_NOT_EXISTS = 1,
		/// <summary>
		/// 状态不能设置为“启用”
		/// </summary>
		[Description("状态不能设置为“启用”")]
		STATUS_CANNOT_SET_TO_ENABLE,
		/// <summary>
		/// 状态不能设置为“禁用”
		/// </summary>
		[Description("状态不能设置为“禁用”")]
		STATUS_CANNOT_SET_TO_DISABLE,
		/// <summary>
		/// 所属课程不存在
		/// </summary>
		[Description("所属课程不存在")]
		COURSE_NOT_EXISTS,
		/// <summary>
		/// 父章节不存在
		/// </summary>
		[Description("父章节不存在")]
		PAREND_NOT_EXISTS,
		/// <summary>
		/// 标题不能为空
		/// </summary>
		[Description("标题不能为空")]
		TITLE_CANNOT_EMPTY,
		/// <summary>
		/// 内容不能为空
		/// </summary>
		[Description("内容不能为空")]
		CONTENT_CANNOT_EMPTY,
		/// <summary>
		/// 不允许被删除
		/// </summary>
		[Description("不允许被删除")]
		CANNOT_DELETE,
		/// <summary>
		/// 不允许编辑
		/// </summary>
		[Description("不允许编辑")]
		CANNOT_MODIFY
	}

	/// <summary>
	/// 编辑数据状态
	/// </summary>
	internal class ChapterModifyState
	{
		/// <summary>
		/// 章节归属课程
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 章节节点
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// 章节标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 章节内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 章节视频
		/// </summary>
		public string Video { get; set; }

		/// <summary>
		/// 章节状态
		/// </summary>
		public int Status { get; set; }
	}

	internal class ChapterManage : StudyProgressEntityBase<long, ChapterManageFailureRule>
	{
		#region 属性

		/// <summary>
		/// 当前课程章节对象
		/// </summary>
		public readonly Data.Entity.Chapter Chapter;

		#endregion

		#region 实例构造

		public ChapterManage(long chapterId)
		{
			ID = chapterId;
		}

		public ChapterManage(Data.Entity.Chapter chapter)
		{
			Chapter = chapter;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (Chapter == null) AddBrokenRule(ChapterManageFailureRule.CHAPTER_NOT_EXISTS);
		}

		#endregion

		#region 操作方法

		public bool CanSetEnable()
		{
			return Chapter != null && Chapter.Status == (int)ChapterStatus.DISABLED;
		}

		public bool CanSetDisable()
		{
			return Chapter != null && Chapter.Status == (int)ChapterStatus.ENABLED;
		}

		public bool CanDelete()
		{
			return true;
		}

		public bool CanModify()
		{
			return true;
		}

		public bool SetEnable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(ChapterManageFailureRule.STATUS_CANNOT_SET_TO_ENABLE);
			});

			bool success= ChapterAccessor.SetStatus(ID, (int)ChapterStatus.ENABLED);

			if (success) ComputeStudyProgress(Chapter.CourseId);

			return success;
		}

		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetDisable()) AddBrokenRule(ChapterManageFailureRule.STATUS_CANNOT_SET_TO_DISABLE);
			});

			bool success= ChapterAccessor.SetStatus(ID, (int)ChapterStatus.DISABLED);

			if (success) ComputeStudyProgress(Chapter.CourseId);

			return success;
		}

		public bool Remove()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanDelete()) AddBrokenRule(ChapterManageFailureRule.CANNOT_DELETE);
			});

			bool success= ChapterAccessor.Remove(ID);

			if (success) ComputeStudyProgress(Chapter.CourseId);

			return success;
		}

		/// <summary>
		/// 修改章节信息
		/// </summary>
		/// <param name="state">编辑后的数据状态</param>
		/// <returns></returns>
		public bool Modify(ChapterModifyState state)
		{
			if (state == null) return false;

			Data.Entity.Chapter parent = null;
			if (state.ParentId > 0) parent = ChapterAccessor.Get(state.ParentId);

			ThrowExceptionIfValidateFailure(() =>
			{
				if (CanModify())
				{
					//所属课程
					if (!CourseAccessor.Exists(state.CourseId)) AddBrokenRule(ChapterManageFailureRule.COURSE_NOT_EXISTS);

					//有关联父章节时,父章节不存在
					if (state.ParentId > 0 && parent == null)
					{
						AddBrokenRule(ChapterManageFailureRule.PAREND_NOT_EXISTS);
					}

					//标题不能为空
					if (string.IsNullOrWhiteSpace(state.Title)) AddBrokenRule(ChapterManageFailureRule.TITLE_CANNOT_EMPTY);

					//内容不能为空
					if (string.IsNullOrWhiteSpace(state.Content)) AddBrokenRule(ChapterManageFailureRule.CONTENT_CANNOT_EMPTY);
				}
				else
				{
					AddBrokenRule(ChapterManageFailureRule.CANNOT_MODIFY);
				}
			});

			bool success= ChapterAccessor.Update(
				ID,
				state.CourseId,
				state.ParentId,
				ChapterTools.CreateParentPath(parent, ID),
				state.Title,
				state.Content,
				state.Video,
				state.Status);

			if (success && (Chapter.CourseId!=state.CourseId || Chapter.Status != state.Status)) ComputeStudyProgress(state.CourseId);

			return success;
		}

		/// <summary>
		/// 获取课程标题
		/// </summary>
		/// <returns></returns>
		public string GetCourseTitle()
		{
			return CourseAccessor.GetTitle(Chapter.CourseId);
		}

		/// <summary>
		/// 获取父章节标题
		/// </summary>
		/// <returns></returns>
		public string GetParentChapterTitle()
		{
			if (Chapter.ParentId > 0)
			{
				return ChapterAccessor.GetTitle(Chapter.ParentId);
			}

			return string.Empty;
		}

		#endregion
	}
}
