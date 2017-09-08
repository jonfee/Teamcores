using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;
using TeamCores.Models;
using System.Linq;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 题目仓储服务
	/// </summary>
	public static class QuestionsAccessor
	{
		/// <summary>
		/// 添加题目到库
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		public static bool Insert(Questions question)
		{
			using (var db = new DataContext())
			{
				db.Questions.Add(question);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 根据题目ID获取题目信息
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		public static Questions Get(long questionId)
		{
			using (var db = new DataContext())
			{
				return db.Questions.Find(questionId);
			}
		}

		/// <summary>
		/// 设置题目状态
		/// </summary>
		/// <param name="questionId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public static bool SetStatus(long questionId, int status)
		{
			using (var db = new DataContext())
			{
				var item = db.Questions.Find(questionId);

				item.Status = status;

				db.Questions.Update(item);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 更新题目信息
		/// </summary>
		/// <param name="questionId">题目ID</param>
		/// <param name="courseId">课程ID</param>
		/// <param name="subjectId">科目ID</param>
		/// <param name="type">题目类型</param>
		/// <param name="marking">是否需要阅卷</param>
		/// <param name="topic">题目标题</param>
		/// <param name="answer">答案选项</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public static bool Update(long questionId, long courseId, long subjectId, int type, bool marking, string topic, string answer, int status)
		{
			using (var db = new DataContext())
			{
				var item = db.Questions.Find(questionId);

				item.CourseId = courseId;
				item.SubjectId = subjectId;
				item.Type = type;
				item.Marking = marking;
				item.Topic = topic;
				item.Answer = answer;
				item.Status = status;

				db.Questions.Update(item);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 删除题目
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		public static bool Delete(long questionId)
		{
			using (var db = new DataContext())
			{
				var item = db.Questions.Find(questionId);

				db.Questions.Remove(item);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 获取指定题目的数据集合
		/// </summary>
		/// <param name="questionIds">题目ID集</param>
		/// <returns></returns>
		public static List<Questions> GetAllFor(IEnumerable<long> questionIds)
		{
			if (questionIds == null || questionIds.Count() < 1) return new List<Questions>();

			var list = new List<Questions>();

			using (var db = new DataContext())
			{
				list = (from p in db.Questions
						where questionIds.Contains(p.QuestionId)
						select p).ToList();
			}

			return list;
		}

		/// <summary>
		/// 获取指定题目的ID及答案键值集合
		/// </summary>
		/// <param name="questionIds">题目ID集合</param>
		/// <returns></returns>
		public static Dictionary<long, string> GetAnswersFor(IEnumerable<long> questionIds)
		{
			var result = new Dictionary<long, string>();

			if (questionIds != null && questionIds.Count() > 0)
			{
				using (var db = new DataContext())
				{
					result = (from p in db.Questions
							  where questionIds.Contains(p.QuestionId)
							  select p).ToDictionary(k => k.QuestionId, v => v.Answer);
				}
			}

			return result;
		}

		/// <summary>
		/// 获取指定课程下的所有题目
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <returns></returns>
		public static List<Questions> GetAllFor(long courseId,int? status=null)
		{
			var list = new List<Questions>();

			using (var db = new DataContext())
			{
				var query = (from p in db.Questions
						where p.CourseId == courseId
						select p);

				if (status.HasValue)
				{
					query = query.Where(p => p.Status == status.Value);
				}

				list = query.ToList();
			}

			return list;
		}

		/// <summary>
		/// 获取课程下的题目集合
		/// </summary>
		/// <param name="courseId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public static List<QuestionSimpleInfo> GetSimpleAllFor(long courseId, int? status = null)
		{
			var list = new List<QuestionSimpleInfo>();

			using (var db = new DataContext())
			{
				var query = (from p in db.Questions
						where p.CourseId == courseId
						select new QuestionSimpleInfo
						{
							CourseId = p.CourseId,
							QuestionId = p.QuestionId,
							Status = p.Status,
							Topic = p.Topic,
							Type = p.Type
						});

				if (status.HasValue)
				{
					query = query.Where(p => p.Status == status.Value);
				}

				list = query.ToList();
			}

			return list;
		}

		/// <summary>
		/// 获取指定课程下的题目集合
		/// </summary>
		/// <param name="courseIds"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public static List<QuestionSimpleInfo> GetSimpleAllFor(IEnumerable<long> courseIds, int? status = null)
		{
			var list = new List<QuestionSimpleInfo>();

			using (var db = new DataContext())
			{
				var query =(from p in db.Questions
						where courseIds.Contains(p.CourseId)
						select new QuestionSimpleInfo
						{
							CourseId = p.CourseId,
							QuestionId = p.QuestionId,
							Status = p.Status,
							Topic = p.Topic,
							Type = p.Type
						});

				if (status.HasValue)
				{
					query = query.Where(p => p.Status == status.Value);
				}

				list = query.ToList();
			}

			return list;
		}

		/// <summary>
		/// 分页获取题目列表信息
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="keyword"></param>
		/// <param name="courseId">所属课程ID,为null时表示不限制</param>
		/// <param name="questionIds">指定的题目ID</param>
		/// <param name="type">题目类型，为null时表示不限制</param>
		/// <param name="status">状态，为null时表示不限制</param>
		/// <returns></returns>
		public static PagerModel<Questions> Get(PagerModel<Questions> pager, string keyword, int? type, long? courseId = null, IEnumerable<long> questionIds = null, int? status = null)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.Questions
							select p;

				//指定所属课程
				if (courseId.HasValue)
				{
					query = from p in query
							where p.CourseId == courseId.Value
							select p;
				}

				//有指定题目
				if (questionIds != null && questionIds.Count() > 0)
				{
					query = from p in query
							where questionIds.Contains(p.QuestionId)
							select p;
				}

				//题目类型
				if (type.HasValue)
				{
					query = from p in query
							where p.Type == type.Value
							select p;
				}

				//根据关键词查询
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					query = from p in query
							where p.Topic.Contains(keyword)
							select p;
				}

				//指定状态
				if (status.HasValue)
				{
					query = from p in query
							where p.Status.Equals(status.Value)
							select p;
				}

				pager.Count = query.Count();
				pager.Table = query.OrderByDescending(p => p.CreateTime).Skip((pager.Index - 1) * pager.Size).Take(pager.Size).ToList();
				return pager;
			}
		}
	}
}
