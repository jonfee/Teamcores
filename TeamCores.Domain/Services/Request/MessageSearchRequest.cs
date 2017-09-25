namespace TeamCores.Domain.Services.Request
{
	/// <summary>
	/// 消息搜索请求
	/// </summary>
	public class MessageSearchRequest
	{
		public long ReceiverId { get; set; }

		public bool? IsReaded { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
	}
}
