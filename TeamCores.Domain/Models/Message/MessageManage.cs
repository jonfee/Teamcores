using System;
using System.ComponentModel;
using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Models.Message
{
	internal enum MessageManageFailureRule
	{
		/// <summary>
		/// 消息不存在
		/// </summary>
		[Description("消息不存在")]
		Message_NOT_EXISTS = 1,
		/// <summary>
		/// 不能设置为”已读“
		/// </summary>
		[Description("不能设置为”已读“")]
		CANNOT_SET_TO_Readed,
		/// <summary>
		/// 无权查看该消息
		/// </summary>
		[Description("无权查看该消息")]
		NO_ACCESS
	}

	internal class MessageManage : EntityBase<long, MessageManageFailureRule>
	{
		private Data.Entity.Messages _message;
		/// <summary>
		/// 消息
		/// </summary>
		public Data.Entity.Messages Message
		{
			get
			{
				if (_message == null)
				{
					_message = MessagesAccessor.GetMessageFor(ID);
				}

				return _message;
			}
		}

		public MessageManage(long messageId)
		{
			ID = messageId;
		}

		protected override void Validate()
		{
			if (Message == null) AddBrokenRule(MessageManageFailureRule.Message_NOT_EXISTS);
		}

		/// <summary>
		/// 读取消息，如果该消息为未读状态，执行此操作后消息会变为已读状态
		/// </summary>
		/// <param name="userId">提取该消息的用户</param>
		/// <returns></returns>
		public Data.Entity.Messages Read(long userId)
		{
			ThrowExceptionIfValidateFailure(()=>
			{
				if (Message.Receiver != userId) AddBrokenRule(MessageManageFailureRule.NO_ACCESS);
			});

			if (!Message.Readed)
			{
				Message.Readed = true;
				Message.ReadTime = DateTime.Now;
				MessagesAccessor.Update(Message);
			}

			return Message;
		}
	}
}
