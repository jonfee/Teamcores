﻿namespace TeamCores.Web.ViewModel.User
{
	/// <summary>
	/// 用户搜索器模型
	/// </summary>
	public class UserSearcherViewModel
	{
		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
