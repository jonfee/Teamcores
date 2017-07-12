using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Common
{
    public class Keys
    {
        /// <summary>
        /// 用户上下文KEY
        /// </summary>
        public const string UserContext = "UserContext";

        /// <summary>
        /// 用户上下文对应Session Key
        /// </summary>
        public const string UserSession = "UserSession";

        /// <summary>
        /// 用户cookie值
        /// </summary>
        public const string UserCookie = "UserCookie";

        /// <summary>
        /// cookie 加密令牌前缀
        /// </summary>
        public static string TokenPrefix = "Teamcores";
    }
}
