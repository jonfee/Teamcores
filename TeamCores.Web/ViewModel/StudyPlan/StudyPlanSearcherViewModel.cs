namespace TeamCores.Web.ViewModel.StudyPlan
{
	/// <summary>
	/// 学习计划搜索器视图模型
	/// </summary>
	public class StudyPlanSearcherViewModel
    {
		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
