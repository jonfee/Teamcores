using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Events;
using TeamCores.Domain.Services.Response;
using TeamCores.Models;

namespace TeamCores.Domain.Models.Course
{
	/// <summary>
	/// 课程编辑时验证错误结果枚举
	/// </summary>
	internal enum CourseManageFailureRule
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

	internal class CourseManage : StudyProgressEntityBase<long, CourseManageFailureRule>
	{
		#region 属性

		private Data.Entity.Course _course;
		/// <summary>
		/// 当前课程对象
		/// </summary>
		public Data.Entity.Course Course
		{
			get
			{
				if (_course == null)
				{
					_course = CourseAccessor.Get(ID);
				}

				return _course;
			}
		}

		#endregion

		#region 构造实例

		public CourseManage(Data.Entity.Course course)
		{
			if (course != null)
			{
				ID = course.CourseId;
				_course = course;
			}
		}

		public CourseManage(long courseId)
		{
			ID = courseId;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//操作的对象为NULL
			if (Course == null) AddBrokenRule(CourseManageFailureRule.OBJECT_IS_NULL);
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
				if (!CanSetToEnable()) AddBrokenRule(CourseManageFailureRule.STATUS_CANNOT_SET_TO_ENABLED);
			});

			bool success = CourseAccessor.SetStatus(ID, (int)CourseStatus.ENABLED);

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
				if (!CanSetToDisable()) AddBrokenRule(CourseManageFailureRule.STATUS_CANNOT_SET_TO_DISABLED);
			});

			bool success = CourseAccessor.SetStatus(ID, (int)CourseStatus.DISABLED);

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
					if (string.IsNullOrWhiteSpace(state.Title)) AddBrokenRule(CourseManageFailureRule.TITLE_CANNOT_NULL_OR_EMPTY);

					//课程内容为空时
					if (string.IsNullOrWhiteSpace(state.Content)) AddBrokenRule(CourseManageFailureRule.CONTENT_CANNOT_NULL_OR_EMPTY);

					//学习目标为空时
					if (string.IsNullOrWhiteSpace(state.Objective)) AddBrokenRule(CourseManageFailureRule.OBJECTIVE_CANNOT_NULL_OR_EMPTY);

					//所属科目不存在时
					if (!SubjectsAccessor.Exists(state.SubjectId)) AddBrokenRule(CourseManageFailureRule.SUBJECT_NOT_EXISTS);
				}
				else
				{
					AddBrokenRule(CourseManageFailureRule.CANNOT_MODIFY);
				}
			});

			//映射数据实体对象后存储
			var editCourse = TransferNewFor(state);

			bool success = CourseAccessor.Update(editCourse);

			if (success && Course.Status != state.Status) ComputeStudyProgress(ID);

			return success;
		}

		/// <summary>
		/// 获取并转换为<see cref="CourseDetails"/>类型对象
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <returns></returns>
		public CourseDetails ConvertToCourseDetails(long studentId)
		{
			if (Course == null) return null;

			var details = new CourseDetails
			{
				CourseId = Course.CourseId,
				SubjectId = Course.SubjectId,
				Content = Course.Content,
				Image = Course.Image,
				CreateTime = Course.CreateTime,
				Objective = Course.Objective,
				Remarks = Course.Remarks,
				Status = Course.Status,
				Title = Course.Title,
				UserId = Course.UserId,
				StudentId = studentId
			};

			details.SubjectName = GetSubjectName();
			details.Chapters = GetChapterStudyInfoList(studentId);

			return details;
		}

		/// <summary>
		/// 获取所有章节层次结构
		/// </summary>
		/// <returns></returns>
		public List<ChapterTier> GetChapterTiers()
		{
			return ChapterAccessor.GetChapterTiers(ID);
		}

		/// <summary>
		/// 获取学员学习过的章节集合
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <returns></returns>
		public long[] GetStudiedChapterIds(long studentId)
		{
			return StudyRecordAccessor.GetChapterIdsFor(studentId, ID);
		}

		/// <summary>
		/// 获取课程所在科目的名称
		/// </summary>
		/// <returns></returns>
		public string GetSubjectName()
		{
			return SubjectsAccessor.GetName(Course.SubjectId);
		}

		/// <summary>
		/// 获取课程下带层级结构的章节学习情况数据集合，只保留启用状态的章节
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <returns></returns>
		public List<ChapterStudyInfo> GetChapterStudyInfoList(long studentId)
		{
			//课程下的章节结构
			var tiers = GetChapterTiers();

			if (tiers == null || tiers.Count < 1) return null;

			//保留“启用”状态的章节
			tiers = tiers.Where(p => p.Status == (int)ChapterStatus.ENABLED).ToList();

			//用户学习过的章节
			long[] studiedChapterIds = null;
			if (studentId > 0)
			{
				studiedChapterIds = GetStudiedChapterIds(studentId);
			}

			//找到顶级章节
			var roots = tiers.Where(p => p.ParentId == 0);

			//输出课程下的带层次结构的章节学习情况数据集合
			var chapters = new List<ChapterStudyInfo>();

			foreach (var chapter in roots)
			{
				var info = ConvertToChapterStudyInfo(tiers, studiedChapterIds, chapter);

				if (info != null) chapters.Add(info);
			}

			return chapters;
		}

		/// <summary>
		/// 获取课程下的所有题目信息
		/// </summary>
		/// <param name="status">题目状态，为NULL时表示不限制</param>
		/// <returns></returns>
		public List<QuestionSimpleInfo> GetQuestions(int? status = null)
		{
			return QuestionsAccessor.GetSimpleAllFor(ID, status);
		}

		/// <summary>
		/// 将更新的数据状态转换为更新后的课程对象
		/// </summary>
		/// <param name="state"></param>
		private Data.Entity.Course TransferNewFor(CourseModifiedState state)
		{
			var editCourse = new Data.Entity.Course();
			editCourse = Course.CopyTo(editCourse);

			editCourse.SubjectId = state.SubjectId;
			editCourse.Title = state.Title;
			editCourse.Image = state.Image;
			editCourse.Content = state.Content;
			editCourse.Remarks = state.Remarks;
			editCourse.Objective = state.Objective;
			editCourse.Status = state.Status;

			return editCourse;
		}

		/// <summary>
		/// 转换并获取当前指定章节的学习状态信息（含子章节结构）
		/// </summary>
		/// <param name="allTiers">所有章节结构信息集合</param>
		/// <param name="studiedChapterIds">学习过的章节ID集合</param>
		/// <param name="currentChapterId">当前章节ID</param>
		/// <returns></returns>
		private ChapterStudyInfo ConvertToChapterStudyInfo(IEnumerable<ChapterTier> allTiers, long[] studiedChapterIds, ChapterTier current)
		{
			if (allTiers == null || allTiers.Count() < 1) return null;
			
			if (current == null) return null;

			//是否存在已学习过的章节
			bool hasStudiedChapter = studiedChapterIds != null && studiedChapterIds.Length > 0;

			//是否已经学习过
			bool studied = false;

			//当前章节下的直子章节
			var tiers = allTiers.Where(p => p.ParentId == current.ChapterId);

			//直子章节集合
			List<ChapterStudyInfo> children = null;

			if (tiers != null && tiers.Count() > 0)
			{
				children = new List<ChapterStudyInfo>();

				foreach (var tier in tiers)
				{
					studied = hasStudiedChapter && studiedChapterIds.Contains(tier.ChapterId);

					var item = ConvertToChapterStudyInfo(allTiers, studiedChapterIds, tier);

					if (item != null) children.Add(item);
				}
			}

			return new ChapterStudyInfo
			{
				ChapterId = current.ChapterId,
				Title = current.Title,
				Studied = hasStudiedChapter && studiedChapterIds.Contains(current.ChapterId),
				Children = children
			}; ;
		}

		#endregion
	}
}
