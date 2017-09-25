using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Services.Request;
using TeamCores.Models;

namespace TeamCores.Domain.Models.Message
{
	internal enum MessageSearchFailureRule
	{
		/// <summary>
		/// 页码不是有效范围值
		/// </summary>
		[Description("页码不是有效范围值")]
		PAGE_INDEX_OUTRANGE = 1,
		/// <summary>
		/// 每页展示数不是有效范围值
		/// </summary>
		[Description("每页展示数不是有效范围值")]
		PAGE_SIZE_OUTRANGE,
	}

	internal class MessageSearch : EntityBase<long, MessageSearchFailureRule>
	{
		#region 属性

		public long ReceiverId { get; set; }

		public bool? IsReaded { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion

		public MessageSearch(MessageSearchRequest request)
		{
			if (request != null)
			{
				ReceiverId = request.ReceiverId;
				IsReaded = request.IsReaded;
				PageIndex = request.PageIndex;
				PageSize = request.PageSize;
			}
		}

		#region 实例化

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (PageIndex < 1) AddBrokenRule(MessageSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) AddBrokenRule(MessageSearchFailureRule.PAGE_SIZE_OUTRANGE);
		}

		#endregion

		public PagerModel<Data.Entity.Messages> Search()
		{
			ThrowExceptionIfValidateFailure();

			PagerModel<Data.Entity.Messages> pager = new PagerModel<Data.Entity.Messages>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			MessagesAccessor.GerMessagesFor(pager, ReceiverId, IsReaded);

			return pager;
		}
	}
}
