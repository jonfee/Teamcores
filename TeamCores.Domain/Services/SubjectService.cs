using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Common.Exceptions;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Subject;

namespace TeamCores.Domain.Services
{
	public class SubjectService
	{
		/// <summary>
		/// 添加新科目
		/// </summary>
		/// <param name="newSubject"></param>
		public bool AddSubject(NewSubject newSubject)
		{
			//对象为null时，抛出业务异常
			if (newSubject == null) throw new TeamCoresException(nameof(newSubject), "新增的科目对象不能为NULL。");

			//对象自验证，如果有错误则抛出业务异常
			newSubject.ThrowExceptionIfValidateFailure();

			//数据仓储对象
			Subjects subject = new Subjects
			{
				Count = newSubject.Count,
				CreateTime = DateTime.Now,
				Name = newSubject.Name,
				Status = newSubject.Status,
				SubjectId = newSubject.ID
			};

			//保存新科目到仓储
			return SubjectsAccessor.Insert(subject);
		}
	}
}
