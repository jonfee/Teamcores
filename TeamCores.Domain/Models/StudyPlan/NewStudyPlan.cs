using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Events;

namespace TeamCores.Domain.Models.StudyPlan
{
	/// <summary>
	/// 新学习计划验证失败错误结果枚举
	/// </summary>
	internal enum NewStudyPlanFailureRule
	{
		/// <summary>
		/// 学习计划制定者不存在
		/// </summary>
		[Description("学习计划制定者不存在")]
		CREATOR_NOT_EXISTS = 1,
		/// <summary>
		/// 学习标题不能为空
		/// </summary>
		[Description("学习标题不能为空")]
		TITLE_CANNOT_EMPTY,
		/// <summary>
		/// 学习标题不能重复
		/// </summary>
		[Description("学习标题不能重复")]
		TITLE_CANNOT_REPEAT,
		/// <summary>
		/// 学习内容说明不能为空
		/// </summary>
		[Description("学习内容说明不能为空")]
		CONTENT_CANNOT_EMPTY,
		/// <summary>
		/// 学习计划必须指定学员
		/// </summary>
		[Description("学习计划必须指定学员")]
		STUDENTS_CANNOT_EMPTY,
		/// <summary>
		/// 学习计划必须关联课程
		/// </summary>
		[Description("学习计划必须关联课程")]
		COURSE_CANNOT_EMPTY,
		/// <summary>
		/// 部分课程不存在
		/// </summary>
		[Description("部分课程不存在")]
		PARTOF_COURSE_NOT_EXSITS
	}

	/// <summary>
	/// 新学习计划领域模型
	/// </summary>
	internal class NewStudyPlan : EntityBase<long, NewStudyPlanFailureRule>
	{
		#region 属性

		/// <summary>
		/// 关联的课程
		/// </summary>
		private IEnumerable<long> _courseIds;

		/// <summary>
		/// 学习计划的学员
		/// </summary>
		private IEnumerable<long> _studentIds;

		private Data.Entity.StudyPlan _studyPlan;
		/// <summary>
		/// 学习计划对象
		/// </summary>
		public Data.Entity.StudyPlan StudyPlan { get; set; }

		private IEnumerable<StudyPlanCourse> _courses;
		/// <summary>
		/// 当前计划关联的课程集合
		/// </summary>
		public IEnumerable<StudyPlanCourse> Courses
		{
			get
			{
				if (_courses == null && StudyPlan != null && _courseIds != null)
				{
					_courses = CreateStudyPlanCourseList();
				}

				return _courses;
			}
		}

		private IEnumerable<UserStudyPlan> _studentPlans;
		/// <summary>
		/// 计划内的学员计划集合
		/// </summary>
		public IEnumerable<UserStudyPlan> StudentsPlan
		{
			get
			{
				if (_studentPlans == null && StudyPlan != null && _studentIds != null)
				{
					_studentPlans = CreateUserStudyPlanList();
				}

				return _studentPlans;
			}
		}

		#endregion

		#region 构造函数

		public NewStudyPlan(long userId, string title, string content, IEnumerable<long> courseIds, IEnumerable<long> studentIds)
		{
			ID = IDProvider.NewId;
			_courseIds = courseIds;
			_studentIds = studentIds;

			_studyPlan = new Data.Entity.StudyPlan
			{
				Content = content,
				CreateTime = DateTime.Now,
				PlanId = ID,
				Status = (int)StudyPlanStatus.ENABLED,
				Student = studentIds != null ? studentIds.Count() : 0,
				Title = title,
				UserId = userId
			};
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//计划制定者不存在
			if (!UsersAccessor.Exists(StudyPlan.UserId)) AddBrokenRule(NewStudyPlanFailureRule.CREATOR_NOT_EXISTS);

			//标题为空
			if (string.IsNullOrWhiteSpace(StudyPlan.Title)) AddBrokenRule(NewStudyPlanFailureRule.TITLE_CANNOT_EMPTY);

			//标题已存在
			if (StudyPlanAccessor.Exists(StudyPlan.Title)) AddBrokenRule(NewStudyPlanFailureRule.TITLE_CANNOT_REPEAT);

			//计划内容说明为空
			if (string.IsNullOrWhiteSpace(StudyPlan.Content)) AddBrokenRule(NewStudyPlanFailureRule.CONTENT_CANNOT_EMPTY);

			//未指定学员
			if (_studentIds==null ||_studentIds.Count() < 1) AddBrokenRule(NewStudyPlanFailureRule.STUDENTS_CANNOT_EMPTY);

			//未指定关联课程
			if (_courseIds == null || _courseIds.Count() < 1) AddBrokenRule(NewStudyPlanFailureRule.COURSE_CANNOT_EMPTY);

			//并非所有课程都存在
			if (!CourseAccessor.AllExists(_courseIds)) AddBrokenRule(NewStudyPlanFailureRule.PARTOF_COURSE_NOT_EXSITS);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 保存
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			//验证
			ThrowExceptionIfValidateFailure();

			//入库
			bool success = StudyPlanAccessor.Insert(StudyPlan, Courses, StudentsPlan);

			#region 事件处理

			if (success)
			{
				ExecuteEventsAfterAdded();
			}

			#endregion

			return success;
		}

		/// <summary>
		/// 新增学习计划后的事件处理
		/// </summary>
		private void ExecuteEventsAfterAdded()
		{
			eventsChannels.Clear();

			#region //事件1: 用户正在学习的计划数统计事件

			UserStudingPlansStatisticsEvent studingPlansEvent = new UserStudingPlansStatisticsEvent(new UserStudingPlansStatisticsEventState
			{
				PlanId = ID,
				UserId = StudyPlan.UserId
			});
			eventsChannels.AddEvent(studingPlansEvent);

			#endregion

			#region //事件2: 学员通知

			NewStudyPlanNoticeEvent newPlanNoticeEvent = new NewStudyPlanNoticeEvent(new NewStudyPlanNoticeEventState
			{
				Plan = StudyPlan,
				StudentIds = _studentIds
			});
			eventsChannels.AddEvent(newPlanNoticeEvent);

			#endregion

			eventsChannels.Execute();
		}

		/// <summary>
		/// 生成学习计划的关联课程列表
		/// </summary>
		/// <returns></returns>
		private IEnumerable<StudyPlanCourse> CreateStudyPlanCourseList()
		{
			int sort = 1;
			foreach (var courseId in _courseIds)
			{
				yield return new StudyPlanCourse
				{
					CourseId = courseId,
					CreateTime = StudyPlan.CreateTime,
					PlanId = ID,
					Sort = sort++
				};
			}
		}

		/// <summary>
		/// 生成学员学习计划列表
		/// </summary>
		/// <returns></returns>
		private IEnumerable<UserStudyPlan> CreateUserStudyPlanList()
		{
			foreach (var userId in _studentIds)
			{
				yield return new UserStudyPlan
				{
					CreateTime = StudyPlan.CreateTime,
					PlanId = ID,
					Progress = 0,
					Status = (int)UserStudyPlanStatus.NOTSTARTED,
					UpdateTime = StudyPlan.CreateTime,
					UserId = userId
				};
			}
		}

		#endregion
	}
}
