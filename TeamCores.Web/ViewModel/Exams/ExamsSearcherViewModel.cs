namespace TeamCores.Web.ViewModel.Exams
{
	/// <summary>
	/// 考卷搜索视图模型
	/// </summary>
	public class ExamsSearcherViewModel
	{
		/// <summary>
		/// 课程ID
		/// </summary>
		public long? CourseId { get; set; }

		/// <summary>
		/// 关键词
		/// </summary>
		public string Keyword { get; set; }

		/// <summary>
		/// 考卷类型
		/// </summary>
		public int? Type { get; set; }

		/// <summary>
		/// 考卷状态
		/// </summary>
		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
