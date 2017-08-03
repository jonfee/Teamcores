using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Utility;

namespace TeamCores.Domain.Models.Chapter
{
	/// <summary>
	/// 新课程章节验证错误结果枚举
	/// </summary>
	internal enum NewChapterFailureRule
	{
		/// <summary>
		/// 所属课程不存在
		/// </summary>
		[Description("所属课程不存在")]
		COURSE_NOT_EXISTS = 1,
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
	}

	/// <summary>
	/// 新课程章节业务领域模型
	/// </summary>
	internal class NewChapter : EntityBase<long, NewChapterFailureRule>
	{
		#region 属性

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
		/// 章节学习次数
		/// </summary>
		public int Count => 0;

		/// <summary>
		/// 章节状态
		/// </summary>
		public int Status => (int)ChapterStatus.ENABLED;

		/// <summary>
		/// 父章节
		/// </summary>
		public readonly Data.Entity.Chapter ParentChapter;

		#endregion

		#region 构造实例

		public NewChapter()
		{
			ID = IDProvider.NewId;

			if (ParentId > 0)
			{
				ParentChapter = ChapterAccessor.Get(ParentId);
			}
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//所属课程
			if (!CourseAccessor.Exists(CourseId)) AddBrokenRule(NewChapterFailureRule.COURSE_NOT_EXISTS);

			//有关联父章节时的验证
			if (ParentId > 0)
			{   //父章节不存在
				if (ParentChapter == null)
				{
					AddBrokenRule(NewChapterFailureRule.PAREND_NOT_EXISTS);
				}
			}

			//标题不能为空
			if (string.IsNullOrWhiteSpace(Title)) AddBrokenRule(NewChapterFailureRule.TITLE_CANNOT_EMPTY);

			//内容不能为空
			if (string.IsNullOrWhiteSpace(Content)) AddBrokenRule(NewChapterFailureRule.CONTENT_CANNOT_EMPTY);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 保存章节
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			ThrowExceptionIfValidateFailure();

			Data.Entity.Chapter chapter = new Data.Entity.Chapter
			{
				ChapterId = ID,
				Content = Content,
				Count = Count,
				CourseId = CourseId,
				CreateTime = DateTime.Now,
				ParentId = ParentId,
				ParentPath = ChapterTools.CreateParentPath(ParentChapter,ID),
				Status = Status,
				Title = Title,
				Video = Video,
				IsLeaf = true
			};

			return ChapterAccessor.Insert(chapter);
		}

		#endregion
	}
}
