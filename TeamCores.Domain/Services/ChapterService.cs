using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Models.Chapter;

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
	}
}
