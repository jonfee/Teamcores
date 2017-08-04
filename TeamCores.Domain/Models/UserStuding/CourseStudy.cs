using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Events;

namespace TeamCores.Domain.Models.UserStuding
{
	/// <summary>
	/// 课程章节学习业务验证错误结果枚举
	/// </summary>
	internal enum CourseStudyFailureRule
	{
		/// <summary>
		/// 学员不存在
		/// </summary>
		[Description("学员不存在")]
		STUDENT_NOT_EXISTS = 1,
		/// <summary>
		/// 学员账号已被禁用
		/// </summary>
		[Description("学员账号已被禁用")]
		STUDENT_STATUS_IS_DISABLED,
		/// <summary>
		/// 课程章节不存在
		/// </summary>
		[Description("课程章节不存在")]
		CHAPTER_NOT_EXISTS,
		/// <summary>
		/// 课程章节被停用
		/// </summary>
		[Description("课程章节被停用")]
		CHAPTER_STATUS_IS_DISABLED,
	}

	/// <summary>
	/// 课程章节学习业务领域模型
	/// </summary>
	internal class CourseStudy : EntityBase<long, CourseStudyFailureRule>
	{
		#region 属性

		/// <summary>
		/// 课程章节信息
		/// </summary>
		public readonly Data.Entity.Chapter Chapter;

		/// <summary>
		/// 学员信息
		/// </summary>
		public readonly Data.Entity.Users Student;

		#endregion

		#region 构造函数

		/// <summary>
		/// 实例化<see cref="CourseStudy"/>对象
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <param name="chapterId">学习的课程章节ID</param>
		public CourseStudy(long studentId, long chapterId)
		{
			Chapter = ChapterAccessor.Get(chapterId);
			Student = UsersAccessor.Get(studentId);
		}

		/// <summary>
		/// 实例化<see cref="CourseStudy"/>对象
		/// </summary>
		/// <param name="chapter">课程章节</param>
		/// <param name="student">学员信息</param>
		public CourseStudy(Data.Entity.Users student, Data.Entity.Chapter chapter)
		{
			Chapter = chapter;
			Student = student;
		}

		/// <summary>
		/// 实例化<see cref="CourseStudy"/>对象
		/// </summary>
		/// <param name="chapter">课程章节</param>
		/// <param name="studentId">学员ID</param>
		public CourseStudy(long studentId, Data.Entity.Chapter chapter)
		{
			Chapter = chapter;
			Student = UsersAccessor.Get(studentId);
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//课程章节不存在
			if (Chapter == null)
			{
				AddBrokenRule(CourseStudyFailureRule.CHAPTER_NOT_EXISTS);
			}
			//被禁用
			else if (Chapter.Status == (int)ChapterStatus.DISABLED)
			{
				AddBrokenRule(CourseStudyFailureRule.CHAPTER_STATUS_IS_DISABLED);
			}

			//学员信息不存在
			if (Student == null)
			{
				AddBrokenRule(CourseStudyFailureRule.STUDENT_NOT_EXISTS);
			}
			//被禁用
			else if (Student.Status == (int)UserStatus.DISABLED)
			{
				AddBrokenRule(CourseStudyFailureRule.STUDENT_STATUS_IS_DISABLED);
			}
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 开始学习
		/// </summary>
		public void Studing()
		{
			//检测本次学习是否正常
			ThrowExceptionIfValidateFailure();

			//记录本次学习信息
			StudingRecord record = new StudingRecord(Student.UserId, Chapter);
			bool success = record.Save();

			//执行事件
			if (success)
			{
				EventsChannels.Clear();

				//更新当前学习章节父章节的学习次数
				EventsChannels.AddEvent(new StudyRecordTimesUpdateEvent(new StudyRecordTimesUpdateEventState
				{
					StudentId = Student.UserId,
					ChapterId = Chapter.ChapterId,
					IncludeMySelf = false
				}));

				//更新章节学习次数
				EventsChannels.AddEvent(new ChapterStudyStatisticsEvent(new ChapterStudyStatisticsEventState
				{
					Chapter = Chapter,
					StudentId = Student.UserId
				}));

				//更新学员当前课程有关的学习计划的学习进度
				EventsChannels.AddEvent(new UserStudyProgressUpdateEvent(new UserStudyProgressUpdateEventState
				{
					StudentId = Student.UserId,
					CourseId = Chapter.CourseId
				}));

				EventsChannels.Excute();
			}
		}

		#endregion
	}
}
