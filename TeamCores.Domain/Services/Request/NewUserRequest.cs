using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Services.Request
{
    /// <summary>
    /// 新用户输入
    /// </summary>
   public class NewUserRequest
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 明文密码
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头衔
        /// </summary>
        public string Title { get; set; }

		/// <summary>
		/// 权限集合，如：["A01","A02","B02","C03"]
		/// </summary>
		public string[] Permissions { get; set; }

		/// <summary>
		/// 是否忽略权限
		/// </summary>
		public bool IgnorePermission { get; set; }
	}
}
