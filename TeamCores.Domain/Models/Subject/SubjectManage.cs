using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Models.Subject
{
	/// <summary>
	/// 科目编辑验证错误结果枚举
	/// </summary>
	internal enum SubjectEditFailureRule
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
	internal class SubjectManage : EntityBase<long, SubjectEditFailureRule>
	{
		#region 属性

		private Subjects _subject;
		/// <summary>
		/// 当前操作的科目对象
		/// </summary>
		public Subjects Subject
		{
			get
			{
				if (_subject == null)
				{
					_subject = SubjectsAccessor.Get(ID);
				}

				return _subject;
			}
		}

		private List<Data.Entity.Course> _courses;
		/// <summary>
		/// 科目下的所有课程集合
		/// </summary>
		public List<Data.Entity.Course> Courses
		{
			get
			{
				if (_courses == null)
				{
					_courses = CourseAccessor.GetAllFor(ID);
				}

				return _courses;
			}
		}

		private long[] _courseIds;
		/// <summary>
		/// 科目下的课程ID集合(忽略课程状态)
		/// </summary>
		public long[] CourseIds
		{
			get
			{
				if (_courseIds == null)
				{
					if (_courses == null)
					{
						_courseIds = CourseAccessor.GetCourseIDsFor(ID);
					}
					else
					{
						_courseIds = Courses.Select(p => p.CourseId).ToArray();
					}
				}

				return _courseIds;
			}
		}

		#endregion

		#region 实例化构造函数

		public SubjectManage(Subjects subject)
		{
			if (subject != null)
			{
				ID = subject.SubjectId;
				this._subject = subject;
			}
		}

		public SubjectManage(long subjectId)
		{
			ID = subjectId;
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
			//科目存在，且科目下无课程
			return Subject != null && (CourseIds == null || CourseIds.Length < 1);
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

		/// <summary>
		/// 获取并转换为<see cref="SubjectDetails"/>类型数据对象
		/// </summary>
		/// <returns></returns>
		public SubjectDetails ConvertToSubjectDetails()
		{
			if (Subject == null) return null;

			var details = new SubjectDetails
			{
				Count = _subject.Count,
				Courses = Courses,
				CreateTime = _subject.CreateTime,
				Name = _subject.Name,
				Status = _subject.Status,
				SubjectId = _subject.SubjectId
			};

			return details;
		}

		#endregion
	}
}
