namespace TeamCores.Web.ViewModel.User
{
	public class UserSearcherViewModel
	{
		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
