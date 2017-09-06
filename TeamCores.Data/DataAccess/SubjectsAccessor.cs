using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 科目数据管理者
	/// </summary>
	public static class SubjectsAccessor
	{
		/// <summary>
		/// 检测科目名称是否存在
		/// </summary>
		/// <param name="name">科目名称</param>
		/// <returns></returns>
		public static bool NameExists(string name)
		{
			using (var db = new DataContext())
			{
				return db.Subjects.Count(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) > 0;
			}
		}

		/// <summary>
		/// 检测科目是否存在
		/// </summary>
		/// <param name="subjectId">科目ID</param>
		/// <returns></returns>
		public static bool Exists(long subjectId)
		{
			using (var db = new DataContext())
			{
				return db.Subjects.Find(subjectId) != null;
			}
		}

		/// <summary>
		/// 添加科目
		/// </summary>
		/// <param name="subject">科目对象</param>
		/// <returns></returns>
		public static bool Insert(Subjects subject)
		{
			using (var db = new DataContext())
			{
				db.Subjects.Add(subject);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 根据科目ID获取科目信息
		/// </summary>
		/// <param name="subjectId">科目ID</param>
		/// <returns></returns>
		public static Subjects Get(long subjectId)
		{
			using (var db = new DataContext())
			{
				return db.Subjects.Find(subjectId);
			}
		}

		/// <summary>
		/// 获取科目名称
		/// </summary>
		/// <param name="subjectId">ID</param>
		/// <returns></returns>
		public static string GetName(long subjectId)
		{
			string name = string.Empty;

			using (var db = new DataContext())
			{
				var item = (from p in db.Subjects
							where p.SubjectId == subjectId
							select p).FirstOrDefault();

				name = (from p in db.Subjects
						where p.SubjectId == subjectId
						select p.Name).FirstOrDefault();
			}

			return name;
		}

		/// <summary>
		/// 获取所有指定名称的科目ID
		/// </summary>
		/// <param name="name">科目名称</param>
		/// <returns></returns>
		public static long[] GetIdsFor(string name)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.Subjects
							where p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
							select p.SubjectId;

				return query.ToArray();
			}
		}

		/// <summary>
		/// 设置科目状态
		/// </summary>
		/// <param name="subjectId">科目ID</param>
		/// <param name="status">设置后的状态</param>
		public static bool SetStatus(long subjectId, int status)
		{
			using (var db = new DataContext())
			{
				var subject = db.Subjects.Find(subjectId);

				subject.Status = status;

				db.Subjects.Update(subject);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 修改科目名称
		/// </summary>
		/// <param name="subjectId">科目ID</param>
		/// <param name="newName">新名称</param>
		/// <returns></returns>
		public static bool ModifyNameTo(long subjectId, string newName)
		{
			using (var db = new DataContext())
			{
				var subject = db.Subjects.Find(subjectId);

				subject.Name = newName;

				db.Subjects.Update(subject);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 物理删除科目
		/// </summary>
		/// <param name="subjectId">科目ID</param>
		/// <returns></returns>
		public static bool Delete(long subjectId)
		{
			using (var db = new DataContext())
			{
				var subject = db.Subjects.Find(subjectId);

				db.Subjects.Remove(subject);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 分页获取科目列表信息
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="keyword"></param>
		/// <param name="status">状态，为null时表示不限制</param>
		/// <returns></returns>
		public static PagerModel<Subjects> Get(PagerModel<Subjects> pager, string keyword, int? status)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.Subjects
							select p;
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					query = from p in query
							where p.Name.Contains(keyword)
							select p;
				}

				if (status.HasValue)
				{
					query = from p in query
							where p.Status.Equals(status.Value)
							select p;
				}

				pager.Count = query.Count();
				var list = query.OrderByDescending(p => p.CreateTime).Skip((pager.Index - 1) * pager.Size).Take(pager.Size).ToList();
				pager.Table = list;
				return pager;
			}
		}
	}
}
