using System.Collections.Generic;

namespace TeamCores.Domain.Events
{
    /// <summary>
    /// 领域事件管道集
    /// </summary>
    internal class DomainEventChannels
	{
		/// <summary>
		/// 事件管道
		/// </summary>
		List<DomainEvent> Events = null;

		/// <summary>
		/// 实例化领域事件管理
		/// </summary>
		public DomainEventChannels()
		{
			Events = new List<DomainEvent>();
		}

		/// <summary>
		/// 清空事件
		/// </summary>
		public void Clear()
		{
			Events = new List<DomainEvent>();
		}

		/// <summary>
		/// 向事件管道添加事件
		/// </summary>
		/// <param name="event"></param>
		public void AddEvent(DomainEvent @event)
		{
			Events.Add(@event);
		}

		/// <summary>
		/// 从事件管理中移除事件
		/// </summary>
		/// <param name="event"></param>
		public void RemoveEvent(DomainEvent @event)
		{
			Events.Remove(@event);
		}

		/// <summary>
		/// 执行事件管道上的所有事件
		/// </summary>
		public void Excute()
		{
			foreach (var @event in Events)
			{
				@event.Execute();
			}
		}
	}
}
