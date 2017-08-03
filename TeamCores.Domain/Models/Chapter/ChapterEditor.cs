using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Utility;

namespace TeamCores.Domain.Models.Chapter
{
	/// <summary>
	/// 课程章节编辑验证错误结果枚举
	/// </summary>
	internal enum ChapterEditFailureRule
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

	internal class ChapterEditor : EntityBase<long, ChapterEditFailureRule>
	{
		#region 属性

		/// <summary>
		/// 当前课程章节对象
		/// </summary>
		public readonly Data.Entity.Chapter Chapter;

		#endregion

		#region 实例构造

		public ChapterEditor(long chapterId)
		{
			ID = chapterId;
		}

		public ChapterEditor(Data.Entity.Chapter chapter)
		{
			Chapter = chapter;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (Chapter == null) AddBrokenRule(ChapterEditFailureRule.CHAPTER_NOT_EXISTS);
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
				if (!CanSetEnable()) AddBrokenRule(ChapterEditFailureRule.STATUS_CANNOT_SET_TO_ENABLE);
			});

			return ChapterAccessor.SetStatus(ID, (int)ChapterStatus.ENABLED);
		}

		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetDisable()) AddBrokenRule(ChapterEditFailureRule.STATUS_CANNOT_SET_TO_DISABLE);
			});

			return ChapterAccessor.SetStatus(ID, (int)ChapterStatus.DISABLED);
		}

		public bool Remove()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanDelete()) AddBrokenRule(ChapterEditFailureRule.CANNOT_DELETE);
			});

			return ChapterAccessor.Remove(ID);
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
					if (!CourseAccessor.Exists(state.CourseId)) AddBrokenRule(ChapterEditFailureRule.COURSE_NOT_EXISTS);

					//有关联父章节时,父章节不存在
					if (state.ParentId > 0 && parent == null)
					{
						AddBrokenRule(ChapterEditFailureRule.PAREND_NOT_EXISTS);
					}

					//标题不能为空
					if (string.IsNullOrWhiteSpace(state.Title)) AddBrokenRule(ChapterEditFailureRule.TITLE_CANNOT_EMPTY);

					//内容不能为空
					if (string.IsNullOrWhiteSpace(state.Content)) AddBrokenRule(ChapterEditFailureRule.CONTENT_CANNOT_EMPTY);
				}
				else
				{
					AddBrokenRule(ChapterEditFailureRule.CANNOT_MODIFY);
				}
			});

			return ChapterAccessor.Update(
				ID,
				state.CourseId,
				state.ParentId,
				ChapterTools.CreateParentPath(parent, ID),
				state.Title,
				state.Content,
				state.Video,
				state.Status);
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
