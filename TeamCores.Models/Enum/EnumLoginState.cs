using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Models.Enum
{
    public enum EnumLoginState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0,
        /// <summary>
        /// 服务错误
        /// </summary>
        ServerException = 1,
        /// <summary>
        /// 账号错误
        /// </summary>
        AccountError = 2,
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError = 4,
        /// <summary>
        /// 被锁定
        /// </summary>
        LockState = 8
    }
}
