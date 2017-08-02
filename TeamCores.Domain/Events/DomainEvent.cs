using TeamCores.Common.Exceptions;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 领域中的事件抽象基类
	/// </summary>
	internal abstract class DomainEvent
	{
		/// <summary>
		/// 事件执行
		/// </summary>
		public abstract void Execute();

		/// <summary>
		/// 抛出事件异常
		/// </summary>
		/// <param name="event">领域事件</param>
		/// <param name="message">错误消息</param>
		public virtual void Throw(DomainEvent @event, string message)
		{
			throw new TeamCoresEventException(nameof(@event), message);
		}
	}
}
