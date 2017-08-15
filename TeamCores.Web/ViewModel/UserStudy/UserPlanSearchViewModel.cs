namespace TeamCores.Web.ViewModel.UserStudy
{
	/// <summary>
	/// 学员学习计划搜索视图模型
	/// </summary>
	public class UserPlanSearchViewModel
    {
		/// <summary>
		/// 指定学员
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
