using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Chapter;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Utility
{
	/// <summary>
	/// 课程章节辅助操作工具类
	/// </summary>
	internal static class ChapterTools
    {
		/// <summary>
		/// 生成章节层级路径
		/// </summary>
		/// <param name="parent">父章节</param>
		/// <param name="currentChapterId">当前章节ID</param>
		/// <returns></returns>
		public static string CreateParentPath(Chapter parent,long currentChapterId)
		{
			string path = currentChapterId.ToString();

			if (parent != null)
			{
				path = $"{parent.ParentPath},{currentChapterId}";
			}

			return path;
		}

		/// <summary>
		/// 将数据映射为<see cref="ChapterDetails"/>类型对象
		/// </summary>
		/// <param name="chapter"></param>
		/// <returns></returns>
		public static ChapterDetails TransferFor(ChapterManage chapter)
		{
			if (chapter == null) return null;

			var details = new ChapterDetails
			{
				ChapterId = chapter.Chapter.ChapterId,
				Content = chapter.Chapter.Content,
				Count = chapter.Chapter.Count,
				CourseId = chapter.Chapter.CourseId,
				CreateTime = chapter.Chapter.CreateTime,
				ParentId = chapter.Chapter.ParentId,
				Status = chapter.Chapter.Status,
				Title = chapter.Chapter.Title,
				Video = chapter.Chapter.Video
			};

			details.CourseTitle = chapter.GetCourseTitle();
			details.ParentTitle = chapter.GetParentChapterTitle();

			return details;
		}
	}
}
