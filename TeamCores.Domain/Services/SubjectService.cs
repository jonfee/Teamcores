using System;
using TeamCores.Common.Exceptions;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Subject;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 科目相关服务
	/// </summary>
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

		/// <summary>
		/// 删除科目
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public bool Delete(long subjectId)
		{
			SubjectEditor subject = new SubjectEditor(subjectId);

			return subject.Delete();
		}

		/// <summary>
		/// 设置科目为”启用“状态
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public bool SetEnable(long subjectId)
		{
			SubjectEditor subject = new SubjectEditor(subjectId);

			return subject.SetEnable();
		}

		/// <summary>
		/// 设置科目为”禁用“状态
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public bool SetDisable(long subjectId)
		{
			SubjectEditor subject = new SubjectEditor(subjectId);

			return subject.SetDisable();
		}

		/// <summary>
		/// 修改科目名称
		/// </summary>
		/// <param name="subjectId"></param>
		/// <param name="newName"></param>
		/// <returns></returns>
		public bool Rename(long subjectId, string newName)
		{
			SubjectEditor subject = new SubjectEditor(subjectId);

			return subject.Rename(newName);
		}

		/// <summary>
		/// 搜索科目信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="keyword"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public PagerModel<Subjects> Search(int pageSize, int pageIndex, string keyword, int? status)
		{
			SubjectSearch search = new SubjectSearch(pageIndex, pageSize, keyword, status);

			return search.Search();
		}
	}
}
