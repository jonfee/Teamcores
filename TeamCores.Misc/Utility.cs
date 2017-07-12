using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Misc
{
    public class Utility
    {
        /// <summary>
        /// 获取用户上下文
        /// </summary>
        /// <returns></returns>
        public static UserContext GetUserContext()
        {
            return UserContext.Standby(MiddlewareConfig.HttpContext);
        }
    }
}
