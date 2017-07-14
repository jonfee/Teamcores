using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Common
{
	/// <summary>
	/// TeamCores系统的专用异常
	/// </summary>
	public class TeamCoresException : Exception
	{
		/// <summary>
		/// 业务错误信息
		/// </summary>
		public readonly Dictionary<object, object> Errors;

		public TeamCoresException() : base() { }

		public TeamCoresException(string message) : base(message) { }

		public TeamCoresException(string message, Dictionary<object, object> errors) : base(message)
		{
			this.Errors = errors;
		}
	}
}
