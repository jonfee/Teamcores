using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Common;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Models
{
    /// <summary>
    /// 新用户业务验证规则结果
    /// </summary>
	public enum NewUserFailureRules
	{
        /// <summary>
        /// 用户名不能为空
        /// </summary>
        [Description("用户名不能为空")]
        USERNAME_REQUIRE = 1,
        /// <summary>
        /// 用户名已被使用
        /// </summary>
        [Description("用户名已被使用")]
        USERNAME_EXISTS,
        /// <summary>
        /// 不是有效的电子邮箱地址
        /// </summary>
		[Description("不是有效的电子邮箱地址")]
        EMAIL_ERROR,
        /// <summary>
        /// 电子邮箱已被使用
        /// </summary>
		[Description("电子邮箱已被使用")]
        EMAIL_EXISTS,
        /// <summary>
        /// 不是有效的中国大陆手机号码
        /// </summary>
		[Description("不是有效的中国大陆手机号码")]
        MOBILE_ERROR,
        /// <summary>
        /// 手机号码已被使用
        /// </summary>
		[Description("手机号码已被使用")]
        MOBILE_EXISTS,
        /// <summary>
        /// 密码必须设置
        /// </summary>
		[Description("密码必须设置")]
        PASSWORD_REQUIRE,
        /// <summary>
        /// 姓名不能为空
        /// </summary>
		[Description("姓名不能为空")]
        NAME_REQUIRE
	}

	/// <summary>
	/// 用户帐户领域对象
	/// </summary>
	public class NewUser : EntityBase<long, NewUserFailureRules>
	{

        #region  属性

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
		/// 密码加密后的密文
		/// </summary>
		public string EncryptPassword
		{
			get
			{
				return Password.PasswordEncrypt();
			}
		}

		/// <summary>
		/// 用户名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 头衔
		/// </summary>
		public string Title { get; set; }

        #endregion

        public NewUser()
        {
            this.ID = IDProvider.NewId;
        }

        #region 自定义验证

        /// <summary>
        /// 检测用户名是否被使用
        /// </summary>
        /// <returns></returns>
        public bool CheckForUsername()
        {
            return UsersAccessor.UsernameExists(Username);
        }

        /// <summary>
        /// 检测邮箱是否被使用
        /// </summary>
        /// <returns></returns>
        public bool CheckForEmail()
        {
            return UsersAccessor.EmailExists(Email);
        }

        /// <summary>
        /// 检测手机号是否被使用
        /// </summary>
        /// <returns></returns>
        public bool CheckForMobile()
        {
            return UsersAccessor.MobileExists(Mobile);
        }

        #endregion

        #region 验证

        protected override void Validate()
		{
			// 用户名不能为空
			if (string.IsNullOrWhiteSpace(Username)) AddBrokenRule(NewUserFailureRules.USERNAME_REQUIRE);

			// 密码不能为空
			if (string.IsNullOrWhiteSpace(Password)) AddBrokenRule(NewUserFailureRules.PASSWORD_REQUIRE);

			// 姓名不能为空
			if (string.IsNullOrWhiteSpace(Name)) AddBrokenRule(NewUserFailureRules.NAME_REQUIRE);

			// Email不能为空且格式要正确
			if (!Email.IsEmail()) AddBrokenRule(NewUserFailureRules.EMAIL_ERROR);

			// 手机号不能为空且格式要正确
			if (!Mobile.IsCnPhone()) AddBrokenRule(NewUserFailureRules.MOBILE_ERROR);

            // 用户名被使用
            if (CheckForUsername()) AddBrokenRule(NewUserFailureRules.USERNAME_EXISTS);

            // 邮箱被使用
            if (CheckForEmail()) AddBrokenRule(NewUserFailureRules.EMAIL_EXISTS);

            // 手机号被使用
            if (CheckForMobile()) AddBrokenRule(NewUserFailureRules.MOBILE_EXISTS);
        }

        #endregion
    }
}
