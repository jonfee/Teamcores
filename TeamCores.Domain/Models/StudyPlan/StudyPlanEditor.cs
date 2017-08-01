using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.StudyPlan
{
	internal enum StudyPlanEditFailureRule
	{
		/// <summary>
		/// 学习计划不存在
		/// </summary>
		[Description("学习计划不存在")]
		STUDYPLAN_NOT_EXISTS = 1,
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
		/// 删除操作不被允许
		/// </summary>
		[Description("删除操作不被允许")]
		CANNOT_DELETE,
		/// <summary>
		/// 编辑操作不被允许
		/// </summary>
		[Description("编辑操作不被允许")]
		CANNOT_EDIT,
	}


	internal class StudyPlanEditor : EntityBase<long, StudyPlanEditFailureRule>
	{
		#region 属性

		/// <summary>
		/// 学习计划数据对象
		/// </summary>
		public readonly Data.Entity.StudyPlan StudyPlan;

		#endregion

		#region 构造函数

		public StudyPlanEditor(long planId)
		{
			ID = planId;
			StudyPlan = StudyPlanAccessor.Get(planId);
		}

		public StudyPlanEditor(Data.Entity.StudyPlan studyPlan)
		{
			if (studyPlan != null)
			{
				ID = studyPlan.PlanId;
				StudyPlan = studyPlan;
			}
		}

		#endregion

		#region 验证
		protected override void Validate()
		{
			//学习计划不存在
			if (StudyPlan == null) AddBrokenRule(StudyPlanEditFailureRule.STUDYPLAN_NOT_EXISTS);
		}

		#endregion

		#region 操作方法

		public bool CanSetEnable()
		{
			return StudyPlan != null && StudyPlan.Status == (int)StudyPlanStatus.DISABLED;
		}

		public bool CanSetDisable()
		{
			return StudyPlan != null && StudyPlan.Status == (int)StudyPlanStatus.ENABLED;
		}

		public bool CanModify()
		{
			return false;
		}

		public bool CanDelete()
		{
			return false;
		}

		public bool SetEnable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(StudyPlanEditFailureRule.STATUS_CANNOT_SET_TO_ENABLE);
			});

			return StudyPlanAccessor.SetStatus(ID, (int)StudyPlanStatus.ENABLED);
		}

		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetDisable()) AddBrokenRule(StudyPlanEditFailureRule.STATUS_CANNOT_SET_TO_DISABLE);
			});

			return StudyPlanAccessor.SetStatus(ID, (int)StudyPlanStatus.DISABLED);
		}

		#endregion

	}
}
