using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 消息仓储管理器
	/// </summary>
	public class MessagesAccessor
	{
		/// <summary>
		/// 插入一条消息数据
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public static bool Add(Messages message)
		{
			if (message == null) return false;

			using (var db = new DataContext())
			{
				db.Messages.Add(message);

				return db.SaveChanges() == 1;
			}
		}

		/// <summary>
		/// 批量插入消息数据，并返回影响的行数
		/// </summary>
		/// <param name="messages"></param>
		/// <returns></returns>
		public static int AddRange(IEnumerable<Messages> messages)
		{
			if (messages == null && messages.Count() < 1) return 0;

			int rows = 0;

			using (var db = new DataContext())
			{
				db.Messages.AddRange(messages);

				rows = db.SaveChanges();
			}

			return rows;
		}

		/// <summary>
		/// 获取消息信息
		/// </summary>
		/// <param name="messageId"></param>
		/// <returns></returns>
		public static Messages GetMessageFor(long messageId)
		{
			using (var db = new DataContext())
			{
				return db.Messages.Find(messageId);
			}
		}

		/// <summary>
		/// 获取用户收到的消息列表
		/// </summary>
		/// <param name="searcher">分布搜索器</param>
		/// <param name="receiverId">接收者用户ID</param>
		/// <param name="isReaded">是否已阅读，为NULL时忽略</param>
		/// <returns></returns>
		public static PagerModel<Messages> GerMessagesFor(PagerModel<Messages> searcher, long receiverId, bool? isReaded)
		{
			if (searcher == null) throw new ArgumentNullException(nameof(searcher));

			if (searcher.Index < 1 || searcher.Size < 1) return searcher;

			var list = new List<Messages>();
			var total = 0;

			using (var db = new DataContext())
			{
				var query = from p in db.Messages
							where p.Receiver == receiverId
							select p;

				if (isReaded.HasValue)
				{
					query = query.Where(p => p.Readed == isReaded.Value);
				}

				total = query.Count();

				list=query.OrderByDescending(p => p.CreateTime).Skip((searcher.Index - 1) * searcher.Size).Take(searcher.Size).ToList();
			}

			searcher.Count = total;
			searcher.Table = list;

			return searcher;
		}

		/// <summary>
		/// 更新消息
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public static bool Update(Messages message)
		{
			if (message == null) return false;

			bool success = false;

			using (var db = new DataContext())
			{
				db.Messages.Update(message);

				success = db.SaveChanges() == 1;
			}

			return success;
		}
	}
}
