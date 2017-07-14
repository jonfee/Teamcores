using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Common.Utilities;

namespace TeamCores.Domain.Models
{
	public enum UserAccountRules
	{
		UsernameRequire = 1,
		EmailError,
		MobileError,
		PasswordRequire,
		NameRequire
	}

	/// <summary>
	/// 用户帐户领域对象
	/// </summary>
	public class NewUser : EntityBase<long, UserAccountRules>
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

		protected override void Validate()
		{
			// 用户名不能为空
			if (string.IsNullOrWhiteSpace(Username)) AddBrokenRule(UserAccountRules.UsernameRequire);

			// 密码不能为空
			if (string.IsNullOrWhiteSpace(Password)) AddBrokenRule(UserAccountRules.PasswordRequire);

			// 姓名不能为空
			if (string.IsNullOrWhiteSpace(Name)) AddBrokenRule(UserAccountRules.NameRequire);

			// Email不能为空且格式要正确
			if (!Email.IsEmail()) AddBrokenRule(UserAccountRules.EmailError);

			// 手机号不能为空且格式要正确
			if (!Mobile.IsCnPhone()) AddBrokenRule(UserAccountRules.MobileError);
		}
	}
}
