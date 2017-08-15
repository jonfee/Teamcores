namespace TeamCores.Web.ViewModel.UserExam
{
	/// <summary>
	/// 用户考卷搜索器视图模型
	/// </summary>
	public class UserExamSearcherViewModel
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

	/// <summary>
	/// 当前用户考卷搜索器视图模型
	/// </summary>
	public class MyExamSearcherViewModel
	{
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
