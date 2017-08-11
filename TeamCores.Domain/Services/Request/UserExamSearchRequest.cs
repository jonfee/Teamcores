namespace TeamCores.Domain.Services.Request
{
	/// <summary>
	/// 用户考卷搜索请求
	/// </summary>
	public class UserExamSearchRequest
    {
		/// <summary>
		/// 考生ID
		/// </summary>
		public long? StudentId { get; set; }

		/// <summary>
		/// 考卷模板ID
		/// </summary>
		public long? ExamId { get; set; }

		/// <summary>
		/// 阅卷状态
		/// </summary>
		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
