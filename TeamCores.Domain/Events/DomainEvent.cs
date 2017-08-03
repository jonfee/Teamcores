using System;
using System.Reflection;
using TeamCores.Common.Exceptions;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 领域事件的数据对象状态
	/// </summary>
	internal abstract class DomainEventState
	{

	}

	/// <summary>
	/// 领域中的事件抽象基类
	/// </summary>
	internal abstract class DomainEvent
	{
		protected DomainEventState State;

		public DomainEvent(DomainEventState state)
		{
			State = state;
		}

		/// <summary>
		/// 事件执行
		/// </summary>
		public abstract void Execute();

		/// <summary>
		/// 抛出事件异常
		/// </summary>
		/// <param name="message">错误消息</param>
		protected virtual void Throw(string message)
		{
			throw new TeamCoresEventException(GetType().Name, message);
		}

		/// <summary>
		/// 事件数据状态验证，事件依赖的数据状态为NULL时，抛出<see cref="TeamCoresEventException"/>类型异常
		/// </summary>
		/// <param name="customValidater">自定义验证器</param>
		protected virtual void Validate(Action customValidater = null)
		{
			if (State == null) Throw("事件依赖的数据状态不能为NULL。");

			customValidater?.Invoke();
		}
	}
}
