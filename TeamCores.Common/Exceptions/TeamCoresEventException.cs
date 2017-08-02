namespace TeamCores.Common.Exceptions
{
	/// <summary>
	/// TeamCores事件异常
	/// </summary>
	public class TeamCoresEventException : TeamCoresException
	{
		/// <summary>
		/// 初始化异常实例
		/// </summary>
		/// <param name="eventName">事件名称</param>
		/// <param name="message">异常消息</param>
		public TeamCoresEventException(string eventName, string message) : base($"EventName:{eventName}。Message:{message}") { }
	}
}
