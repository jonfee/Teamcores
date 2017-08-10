﻿using System;
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
        /// 制定学习计划用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 学习计划标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 计划说明
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status => (int)StudyPlanStatus.ENABLED;

        /// <summary>
        /// 学员ID集合
        /// </summary>
        public IEnumerable<long> Students { get; set; }

        /// <summary>
        /// 学员数量
        /// </summary>
        public int StudentCount => Students != null ? Students.Count() : 0;

        /// <summary>
        /// 关联的课程
        /// </summary>
        public IEnumerable<long> CourseIds { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime => DateTime.Now;

        #endregion

        #region 构造函数

        public NewStudyPlan()
        {
            ID = IDProvider.NewId;
        }

        #endregion

        #region 验证

        protected override void Validate()
        {
            //计划制定者不存在
            if (!UsersAccessor.Exists(UserId)) AddBrokenRule(NewStudyPlanFailureRule.CREATOR_NOT_EXISTS);

            //标题为空
            if (string.IsNullOrWhiteSpace(Title)) AddBrokenRule(NewStudyPlanFailureRule.TITLE_CANNOT_EMPTY);

            //标题已存在
            if (StudyPlanAccessor.Exists(Title)) AddBrokenRule(NewStudyPlanFailureRule.TITLE_CANNOT_REPEAT);

            //计划内容说明为空
            if (string.IsNullOrWhiteSpace(Content)) AddBrokenRule(NewStudyPlanFailureRule.CONTENT_CANNOT_EMPTY);

            //未指定学员
            if (StudentCount < 1) AddBrokenRule(NewStudyPlanFailureRule.STUDENTS_CANNOT_EMPTY);

            //未指定关联课程
            if (CourseIds == null || CourseIds.Count() < 1) AddBrokenRule(NewStudyPlanFailureRule.COURSE_CANNOT_EMPTY);

            //并非所有课程都存在
            if (!CourseAccessor.AllExists(CourseIds)) AddBrokenRule(NewStudyPlanFailureRule.PARTOF_COURSE_NOT_EXSITS);
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

            var plan = TransForStudyPlan();
            var courseList = CreateStudyPlanCourseList();
            var userPlanList = CreateUserStudyPlanList();

            //入库
            bool success = StudyPlanAccessor.Insert(plan, courseList, userPlanList);

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
			EventsChannels.Clear();

			//用户正在学习的计划数统计事件
			UserStudingPlansStatisticsEvent studingPlansEvent = new UserStudingPlansStatisticsEvent(new UserStudingPlansStatisticsEventState
			{
				PlanId = ID,
				UserId = UserId
			});
			EventsChannels.AddEvent(studingPlansEvent);

			EventsChannels.Execute();
		}

        private Data.Entity.StudyPlan TransForStudyPlan()
        {
            return new Data.Entity.StudyPlan
            {
                Content = Content,
                CreateTime = CreateTime,
                PlanId = ID,
                Status = Status,
                Student = StudentCount,
                Title = Title,
                UserId = UserId
            };
        }

		/// <summary>
		/// 生成学习计划的关联课程列表
		/// </summary>
		/// <returns></returns>
        private IEnumerable<StudyPlanCourse> CreateStudyPlanCourseList()
        {
            int sort = 1;
            foreach (var courseId in CourseIds)
            {
                yield return new StudyPlanCourse
                {
                    CourseId = courseId,
                    CreateTime = CreateTime,
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
            foreach (var userId in Students)
            {
                yield return new UserStudyPlan
                {
                    CreateTime = CreateTime,
                    PlanId = ID,
                    Progress = 0,
                    Status = (int)UserStudyPlanStatus.NOTSTARTED,
                    UpdateTime = CreateTime,
                    UserId = UserId
                };
            }
        }

        #endregion
    }
}
