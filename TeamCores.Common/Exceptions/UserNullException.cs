using System.Collections.Generic;

namespace TeamCores.Common.Exceptions
{
    /// <summary>
    /// 用户对象为NULL引发的异常
    /// </summary>
    public class UserNullException : TeamCoresException
    {
        public UserNullException() : base() { }

        public UserNullException(string message) : base(message) { }

        public UserNullException(string message, string error) : base(message, error) { }

        public UserNullException(string message, Dictionary<object, object> errors) : base(message) { }
    }
}
