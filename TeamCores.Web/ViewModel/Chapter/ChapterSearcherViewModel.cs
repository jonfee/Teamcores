using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCores.Web.ViewModel.Chapter
{
	/// <summary>
	/// 课程章节搜索器视图模型
	/// </summary>
    public class ChapterSearcherViewModel
    {
		public long? CourseId { get; set; }

		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
