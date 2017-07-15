using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Common.Exceptions
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

        /// <summary>
        /// 初始化异常实例
        /// </summary>
        /// <param name="message">异常消息</param>
        public TeamCoresException(string message) : base(message) { }

        /// <summary>
        /// 初始化异常实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="error">业务异常的错误信息，默认以键为"error"记录</param>
        public TeamCoresException(string message, string error) : this(message, new Dictionary<object, object>() { { "ERROR", error } }) { }

        /// <summary>
        /// 初始化异常实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="errors">业务异常的错误信息集合</param>
        public TeamCoresException(string message, Dictionary<object, object> errors) : base(message)
        {
            this.Errors = errors;
        }
    }
}
