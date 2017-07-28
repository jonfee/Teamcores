namespace TeamCores.Web.ViewModel.Course
{
	/// <summary>
	/// 课程搜索器视图模型
	/// </summary>
	public class CourseSearcherViewModel
    {
		public long? SubjectId { get; set; }

		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
