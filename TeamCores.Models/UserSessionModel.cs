using System;

namespace TeamCores.Models
{
	public class UserSessionModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

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
		/// 本次登录时间
		/// </summary>
		public DateTime CurrentLoginTime { get; set; }

		/// <summary>
		/// 本次登录后最后上报的学习时间
		/// </summary>
		public DateTime? LastReportStudyTime { get; set; }
	}
}
