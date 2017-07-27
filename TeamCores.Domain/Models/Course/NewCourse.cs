using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Course
{
	/// <summary>
	/// 新课程验证错误结果枚举
	/// </summary>
	public enum NewCourseFailureRule
	{
		/// <summary>
		/// 创建者不存在
		/// </summary>
		[Description("创建者不存在")]
		CREATER_NOT_EXISTS=1,
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
		OBJECTIVE_CANNOT_NULL_OR_EMPTY
	}

	public class NewCourse : EntityBase<long, NewCourseFailureRule>
	{
		#region  属性

		/// <summary>
		/// 归属科目
		/// </summary>
		public long SubjectId { get; set; }

		/// <summary>
		/// 建立课程用户ID
		/// </summary>
		public long UserId { get; set; }

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
		public readonly int Status;

		#endregion

		#region 构造方法
		public NewCourse()
		{
			ID = IDProvider.NewId;
			Status = (int)CourseStatus.ENABLED;
		}
		#endregion

		#region 验证

		/// <summary>
		/// 科目是否存在
		/// </summary>
		/// <returns></returns>
		public bool SubjectExists()
		{
			if (UserId > 0)
			{
				return UsersAccessor.Exists(UserId);
			}

			return false;
		}

		/// <summary>
		/// 创建者（用户）是否存在
		/// </summary>
		/// <returns></returns>
		public bool CreatorExists()
		{
			if (SubjectId > 0)
			{
				return SubjectsAccessor.Exists(SubjectId);
			}

			return false;
		}

		/// <summary>
		/// 业务数据检测
		/// </summary>
		protected override void Validate()
		{
			//课程标题为空时
			if (string.IsNullOrWhiteSpace(Title)) AddBrokenRule(NewCourseFailureRule.TITLE_CANNOT_NULL_OR_EMPTY);

			//课程内容为空时
			if (string.IsNullOrWhiteSpace(Content)) AddBrokenRule(NewCourseFailureRule.CONTENT_CANNOT_NULL_OR_EMPTY);

			//学习目标为空时
			if (string.IsNullOrWhiteSpace(Objective)) AddBrokenRule(NewCourseFailureRule.OBJECTIVE_CANNOT_NULL_OR_EMPTY);

			//所属科目不存在时
			if (!SubjectExists()) AddBrokenRule(NewCourseFailureRule.SUBJECT_NOT_EXISTS);

			//创建课程的用户不存在时
			if (!CreatorExists()) AddBrokenRule(NewCourseFailureRule.CREATER_NOT_EXISTS);
		}

		#endregion

		#region 操作方法
		
		#endregion
	}
}
