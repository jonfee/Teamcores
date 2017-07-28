using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 课程章节仓储服务
	/// </summary>
	public static class ChapterAccessor
    {
		/// <summary>
		/// 检测章节是否存在
		/// </summary>
		/// <param name="chapterId"></param>
		/// <returns></returns>
		public static bool Exists(long chapterId)
		{
			using (var db = new DataContext())
			{
				return db.Chapter.Count(p => p.ChapterId == chapterId) > 0;
			}
		}

		/// <summary>
		/// 插入课程章节
		/// </summary>
		/// <param name="chapter"></param>
		/// <returns></returns>
		public static bool Insert(Chapter chapter)
		{
			if (chapter == null) return false;

			using (var db = new DataContext())
			{
				db.Chapter.Add(chapter);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 获取课程章节
		/// </summary>
		/// <param name="chapterId">课程章节ID</param>
		/// <returns></returns>
		public static Chapter Get(long chapterId)
		{
			using (var db = new DataContext())
			{
				return db.Chapter.Find(chapterId);
			}
		}

		/// <summary>
		/// 设置状态
		/// </summary>
		/// <param name="chapterId">课程章节ID</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public static bool SetStatus(long chapterId, int status)
		{
			using (var db = new DataContext())
			{
				var item = db.Chapter.Find(chapterId);

				if (item == null) return false;

				item.Status = status;

				db.Chapter.Update(item);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 更新信息
		/// </summary>
		/// <param name="chapterId">章节ID</param>
		/// <param name="courseId">课程ID</param>
		/// <param name="courseId">所属课程ID</param>
		/// <param name="title">章节标题</param>
		/// <param name="content">内容</param>
		/// <param name="parentId">父章节ID</param>
		/// <param name="video">视频地址</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public static bool Update(long chapterId, long courseId,long parentId, string title, string content,string video, int status)
		{
			using (var db = new DataContext())
			{
				var item = db.Chapter.Find(chapterId);

				if (item == null) return false;

				item.CourseId = courseId;
				item.ParentId = parentId;
				item.Title = title;
				item.Video = video;
				item.Content = content;
				item.Status = status;

				db.Chapter.Update(item);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 分页获取章节列表信息
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="keyword">关键词</param>
		/// <param name="courseId">所属课程ID,为null时表示不限制</param>
		/// <param name="status">状态，为null时表示不限制</param>
		/// <returns></returns>
		public static PagerModel<Chapter> Get(PagerModel<Chapter> pager, string keyword, long? courseId = null, int? status = null)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.Chapter
							select p;

				//指定所属科目
				if (courseId.HasValue)
				{
					query = from p in query
							where p.CourseId == courseId.Value
							select p;
				}

				//根据关键词查询
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					query = from p in query
							where p.Title.Contains(keyword)
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
