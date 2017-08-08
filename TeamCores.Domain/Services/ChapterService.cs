using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Chapter;
using TeamCores.Domain.Models.UserStuding;
using TeamCores.Domain.Services.Response;
using TeamCores.Domain.Utility;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 课程章节领域业务服务
	/// </summary>
	public class ChapterService
	{
		/// <summary>
		/// 添加课程章节
		/// </summary>
		/// <param name="courseId">所属课程</param>
		/// <param name="parentId">父章节</param>
		/// <param name="title">标题</param>
		/// <param name="content">内容</param>
		/// <param name="video">视频地址</param>
		/// <returns></returns>
		public bool Add(long courseId, long parentId, string title, string content, string video)
		{
			var chapter = new NewChapter
			{
				CourseId = courseId,
				ParentId = parentId,
				Title = title,
				Content = content,
				Video = video
			};

			return chapter.Save();
		}

		/// <summary>
		/// 设置为“启用”状态
		/// </summary>
		/// <param name="chapterId"></param>
		/// <returns></returns>
		public bool SetEnable(long chapterId)
		{
			var chapter = new ChapterManage(chapterId);

			return chapter.SetEnable();
		}

		/// <summary>
		/// 设置为“禁用”状态
		/// </summary>
		/// <param name="chapterId"></param>
		/// <returns></returns>
		public bool SetDisable(long chapterId)
		{
			var chapter = new ChapterManage(chapterId);

			return chapter.SetDisable();
		}

		/// <summary>
		/// 删除章节
		/// </summary>
		/// <param name="chapterId"></param>
		/// <returns></returns>
		public bool Delete(long chapterId)
		{
			var chapter = new ChapterManage(chapterId);

			return chapter.Remove();
		}

		/// <summary>
		/// 修改章节信息
		/// </summary>
		/// <param name="chapterId">章节ID</param>
		/// <param name="courseId">所属课程</param>
		/// <param name="parentId">父章节ID</param>
		/// <param name="title">标题</param>
		/// <param name="content">内容</param>
		/// <param name="video">视频地址</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public bool Modify(long chapterId, long courseId, long parentId, string title, string content, string video, int status)
		{
			var chapter = new ChapterManage(chapterId);

			var state = new ChapterModifyState
			{
				Content = content,
				CourseId = courseId,
				ParentId = parentId,
				Status = status,
				Title = title,
				Video = video
			};

			return chapter.Modify(state);
		}

		/// <summary>
		/// 搜索课程章节信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="keyword"></param>
		/// <param name="courseId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public PagerModel<Chapter> Search(int pageSize, int pageIndex, string keyword, long? courseId, int? status)
		{
			ChapterSearch search = new ChapterSearch(pageIndex, pageSize, keyword, courseId, status);

			return search.Search();
		}

		/// <summary>
		/// 获取章节详细信息
		/// </summary>
		/// <param name="chapterId">章节ID</param>
		/// <returns></returns>
		public ChapterDetails GetDetails(long chapterId)
		{
			var chapter = new ChapterManage(chapterId);

			var details = ChapterTools.TransferFor(chapter);

			return details;
		}
	}
}
