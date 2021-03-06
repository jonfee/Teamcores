﻿namespace TeamCores.Web.ViewModel.UserStudy
{
	/// <summary>
	/// 当前用户学习计划搜索器视图模型
	/// </summary>
	public class PlanSearcherViewModel
    {

		public int PageSize { get; set; }

		public int PageIndex { get; set; }

		/// <summary>
		/// 学习状态
		/// </summary>
		public int? StudyStatus { get; set; }
	}
}
