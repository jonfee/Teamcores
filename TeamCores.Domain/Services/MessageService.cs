using TeamCores.Domain.Models.Message;
using TeamCores.Domain.Services.Request;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 消息服务
	/// </summary>
	public class MessageService
    {
		/// <summary>
		/// 获取消息信息
		/// </summary>
		/// <param name="messageId"></param>
		/// <returns></returns>
		public Data.Entity.Messages GetMessageFor(long messageId)
		{
			MessageManage manage = new MessageManage(messageId);

			return manage.Message;
		}

		/// <summary>
		/// 读取消息（会影响消息的阅读状态）
		/// </summary>
		/// <param name="messageId"></param>
		/// <returns></returns>
		public Data.Entity.Messages ReadMessasge(long messageId)
		{
			MessageManage manage = new MessageManage(messageId);

			return manage.Read();
		}

		/// <summary>
		/// 搜索消息
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public PagerModel<Data.Entity.Messages> Search(MessageSearchRequest request)
		{
			MessageSearch search = new MessageSearch(request);

			return search.Search();
		}
    }
}
