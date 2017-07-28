using System;
using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Subject
{
	/// <summary>
	/// 科目编辑验证错误结果枚举
	/// </summary>
	public enum SubjectEditFailureRule
	{
		/// <summary>
		/// 科目不存在
		/// </summary>
		[Description("科目不存在")]
		SUBJECT_NOT_EXISTS = 1,
		/// <summary>
		/// 科目名称已存在
		/// </summary>
		[Description("科目名称已存在")]
		NAME_EXISTS = 1,
		/// <summary>
		/// 当前已经是启用状态
		/// </summary>
		[Description("当前已经是启用状态")]
		STATUS_HAS_BEEN_ENABLED,
		/// <summary>
		/// 当前已经是禁用状态
		/// </summary>
		[Description("当前已经是禁用状态")]
		STATUS_HAS_BEEN_DISABLED,
		/// <summary>
		/// 当前科目下有课程
		/// </summary>
		[Description("当前科目下有课程")]
		HAS_COURSES
	}

	/// <summary>
	/// 科目编辑操作领域对象
	/// </summary>
	public class SubjectEditor : EntityBase<long, SubjectEditFailureRule>
	{
		#region 属性

		/// <summary>
		/// 当前操作的科目对象
		/// </summary>
		public Subjects Subject { get; private set; }

		#endregion

		#region 实例化构造函数

		public SubjectEditor(Subjects subject)
		{
			if (subject != null) ID = subject.SubjectId;

			this.Subject = subject;
		}

		public SubjectEditor(long subjectId)
		{
			this.ID = subjectId;

			this.Subject = SubjectsAccessor.Get(this.ID);
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//对象为NULL，是异常数据
			if (Subject == null) this.AddBrokenRule(SubjectEditFailureRule.SUBJECT_NOT_EXISTS);
		}

		#endregion

		#region 操作方法 

		/// <summary>
		/// 是否允许设置为“启用”状态
		/// </summary>
		/// <returns></returns>
		public bool CanSetEnable()
		{
			return Subject != null && Subject.Status == (int)SubjectStatus.DISABLED;
		}

		/// <summary>
		/// 是否允许设置为“禁用”状态
		/// </summary>
		/// <returns></returns>
		public bool CanSetDisable()
		{
			return Subject != null && Subject.Status == (int)SubjectStatus.ENABLED;
		}

		/// <summary>
		/// 是否允许被删除
		/// </summary>
		/// <returns></returns>
		public bool CanDelete()
		{
			return Subject != null && Subject.Count < 1;
		}

		/// <summary>
		/// 是否允许修改名称为指定的名称
		/// </summary>
		/// <param name="newName"></param>
		/// <returns></returns>
		public bool CanModifyNameTo(string newName)
		{
			if (string.IsNullOrWhiteSpace(newName)) return false;

			if (newName.Equals(Subject.Name, StringComparison.OrdinalIgnoreCase)) return true;

			var ids = SubjectsAccessor.GetIdsFor(newName);

			return ids.Length == 0;
		}

		/// <summary>
		/// 设置为“启用“状态
		/// </summary>
		/// <returns></returns>
		public bool SetEnable()
		{
			//业务数据检测，存在错误则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(SubjectEditFailureRule.STATUS_HAS_BEEN_ENABLED);
			});

			return SubjectsAccessor.SetStatus(ID, (int)SubjectStatus.ENABLED);
		}

		/// <summary>
		/// 设置为”禁用“状态
		/// </summary>
		/// <returns></returns>
		public bool SetDisable()
		{
			//业务数据检测，存在错误则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetDisable()) AddBrokenRule(SubjectEditFailureRule.STATUS_HAS_BEEN_DISABLED);
			});

			return SubjectsAccessor.SetStatus(ID, (int)SubjectStatus.DISABLED);
		}

		/// <summary>
		/// 修改科目名称
		/// </summary>
		/// <param name="newName"></param>
		/// <returns></returns>
		public bool Rename(string newName)
		{
			//业务数据检测，存在错误则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanModifyNameTo(newName)) AddBrokenRule(SubjectEditFailureRule.NAME_EXISTS);
			});

			return SubjectsAccessor.ModifyNameTo(ID, newName);
		}

		/// <summary>
		/// 物理删除当前科目
		/// </summary>
		/// <returns></returns>
		public bool Delete()
		{
			//业务数据检测，存在错误则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanDelete()) AddBrokenRule(SubjectEditFailureRule.HAS_COURSES);
			});

			return SubjectsAccessor.Delete(ID);
		}

		#endregion
	}
}
