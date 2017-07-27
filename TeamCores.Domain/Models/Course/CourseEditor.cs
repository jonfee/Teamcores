using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Course
{
	/// <summary>
	/// 课程编辑时验证错误结果枚举
	/// </summary>
	public enum CourseEditFailureRule
	{
		/// <summary>
		/// 当前已经是启用状态
		/// </summary>
		[Description("当前已经是启用状态")]
		OBJECT_IS_NULL=1,
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
	}

	/// <summary>
	/// 课程被修改后的状态
	/// </summary>
	public class CourseModifiedState
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

	public class CourseEditor : EntityBase<long, CourseEditFailureRule>
	{
		#region 属性

		/// <summary>
		/// 当前课程对象
		/// </summary>
		public Data.Entity.Course CourseInfo { get; private set; }

		#endregion

		#region 构造实例

		public CourseEditor(Data.Entity.Course course)
		{
			CourseInfo = course;
		}

		public CourseEditor(long courseId)
		{
			ID = courseId;

			CourseInfo = CourseAccessor.Get(courseId);
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//操作的对象为NULL
			if (CourseInfo == null) AddBrokenRule(CourseEditFailureRule.OBJECT_IS_NULL);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 是否允许设置为“启用”
		/// </summary>
		/// <returns></returns>
		public bool CanSetToEnable()
		{
			return CourseInfo != null && CourseInfo.Status == (int)CourseStatus.DISABLED;
		}

		/// <summary>
		/// 是否允许设置为“禁用”
		/// </summary>
		/// <returns></returns>
		public bool CanSetToDisable()
		{
			return CourseInfo != null && CourseInfo.Status == (int)CourseStatus.ENABLED;
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

			return false;
		}

		/// <summary>
		/// 设置为“禁用”状态
		/// </summary>
		/// <returns></returns>
		public bool SetDisable()
		{
			return false;
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
			return false;
		}

		#endregion
	}
}
