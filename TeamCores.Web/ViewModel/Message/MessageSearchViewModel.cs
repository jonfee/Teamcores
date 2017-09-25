namespace TeamCores.Web.ViewModel.Message
{
	/// <summary>
	/// 消息搜索视图模型
	/// </summary>
	public class MessageSearchViewModel
    {
		public bool? IsReaded { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
