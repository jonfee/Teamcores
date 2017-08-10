namespace TeamCores.Domain.Services.Request
{
	/// <summary>
	/// 考卷搜索请求
	/// </summary>
	public class ExamsSearchRequest
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
		/// 考卷状态
		/// </summary>
		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
