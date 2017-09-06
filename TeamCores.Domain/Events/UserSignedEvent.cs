using System;
using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Events
{
	internal class UserSignedEventState : DomainEventState
	{
		public long UserId { get; set; }

		public DateTime SignedTime { get; set; }

		public string IP { get; set; }
	}

	/// <summary>
	/// 用户登录成功后事件
	/// </summary>
	internal class UserSignedEvent : DomainEvent
	{
		public UserSignedEvent(UserSignedEventState state) : base(state)
		{
		}

		public override void Execute()
		{
			Validate();

			var state = State as UserSignedEventState;

			UsersAccessor.UpdateSignInfo(state.UserId, state.SignedTime);
		}
	}
}
