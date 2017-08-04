namespace TeamCores.Web.ViewModel.UserStudy
{
	/// <summary>
	/// 用户学习计划搜索器视图模型
	/// </summary>
	public class PlanSearcherViewModel
    {
		/// <summary>
		/// 学员ID
		/// </summary>
		public long? StudentId { get; set; }

		public int PageSize { get; set; }

		public int PageIndex { get; set; }

		/// <summary>
		/// 学习状态
		/// </summary>
		public int? StudyStatus { get; set; }
	}
}
