using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCores.Web.ViewModel.Chapter
{
	/// <summary>
	/// 章节编辑视图模型
	/// </summary>
	public class ModifyChapterViewModel
    {
		/// <summary>
		/// 章节ID
		/// </summary>
		public long ChapterId { get; set; }

		/// <summary>
		/// 章节归属课程
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 章节节点
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// 章节标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 章节内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 章节视频
		/// </summary>
		public string Video { get; set; }

		/// <summary>
		/// 章节状态
		/// </summary>
		public int Status { get; set; }
	}
}
