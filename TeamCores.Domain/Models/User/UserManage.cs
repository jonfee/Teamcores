using System;
using System.ComponentModel;

using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.User
{
	/// <summary>
	/// 用户账号信息验证失败规则
	/// </summary>
	public enum UserAccountFailureRules
	{
		/// <summary>
		/// 用户不存在
		/// </summary>
		[Description("用户不存在")]
		USER_NOT_EXISTS = 1,

		/// <summary>
		/// 编辑的数据不能为NULL
		/// </summary>
		[Description("编辑的数据不能为NULL")]
		MODIFYSTATE_CANNOT_NULL,

		/// <summary>
		/// 新密码不能为空
		/// </summary>
		[Description("新密码不能为空")]
		NEW_PASSWORD_CANNOT_NULL,

		/// <summary>
		/// 原密码验证失败
		/// </summary>
		[Description("原密码验证失败")]
		OLD_PASSWORD_CHECK_FAILURE,

		/// <summary>
		/// 用户名不能为空
		/// </summary>
		[Description("用户名不能为空")]
		USERNAME_REQUIRE,

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
		[Description("电子邮箱已被使用")] EMAIL_EXISTS,

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
		/// 姓名不能为空
		/// </summary>
		[Description("姓名不能为空")]
		NAME_REQUIRE,

		/// <summary>
		/// 不能设置为启用
		/// </summary>
		[Description("不能设置为启用")]
		CANNET_SET_ENABLED,

		/// <summary>
		/// 不能设置为禁用
		/// </summary>
		[Description("不能设置为禁用")]
		CANNET_SET_DISABLED
	}

	/// <summary>
	/// 用户编辑数据状态
	/// </summary>
	internal class UserModifyState
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Title { get; set; }
		public string Name { get; set; }
		public string[] Permissions { get; set; }
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
	}

	/// <summary>
	/// 用户账号
	/// </summary>
	internal class UserManage : EntityBase<long, UserAccountFailureRules>
	{
		/// <summary>
		/// 用户账号信息
		/// </summary>
		public Users UserInfo
		{
			get;
			private set;
		}

		public UserManage(Users user)
		{
			if (user != null)
			{
				UserInfo = user;
				ID = user.UserId;
			}
		}

		public UserManage(long userId)
		{
			ID = userId;

			InitUser();
		}

		/// <summary>
		/// 初始化用户信息
		/// </summary>
		private void InitUser()
		{
			UserInfo = UsersAccessor.Get(ID);
		}

		protected override void Validate()
		{
			if (UserInfo == null) AddBrokenRule(UserAccountFailureRules.USER_NOT_EXISTS);
		}

		/// <summary>
		/// 是否允许启用
		/// </summary>
		/// <returns> </returns>
		public bool CanSetEnabled()
		{
			if (null != UserInfo && UserInfo.Status == (int)UserStatus.DISABLED)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// 是否允许禁用
		/// </summary>
		/// <returns> </returns>
		public bool CanSetDisabled()
		{
			if (null != UserInfo && UserInfo.Status == (int)UserStatus.ENABLED)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// 修改用户登录密码
		/// </summary>
		/// <param name = "oldWord" > </param>
		/// <param name = "newWord" > </param>
		/// <returns> </returns>
		public bool ModifyPassword(string oldWord, string newWord)
		{
			bool success = false;

			//如果用户信息验证失败，则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				//新密码不能为空
				if (string.IsNullOrWhiteSpace(newWord)) AddBrokenRule(UserAccountFailureRules.NEW_PASSWORD_CANNOT_NULL);

				//验证旧密码是否正确
				if (string.IsNullOrWhiteSpace(oldWord) || !oldWord.PasswordEncrypt()
						.Equals(UserInfo.Password, StringComparison.OrdinalIgnoreCase))
					AddBrokenRule(UserAccountFailureRules.OLD_PASSWORD_CHECK_FAILURE);
			});

			//新旧密码不一致时，将新密码更新到数据库
			if (!oldWord.Equals(newWord))
			{
				string encryptPwd = newWord.PasswordEncrypt();

				if (encryptPwd.Equals(UserInfo.Password, StringComparison.OrdinalIgnoreCase))
				{
					success = true;
				}
				else
				{
					success = UsersAccessor.ResetPassword(ID, encryptPwd);
				}
			}

			return success;
		}

		/// <summary>
		/// 重置用户登录密码
		/// </summary>
		/// <param name = "newWord" > </param>
		/// <returns> </returns>
		public bool ResetPassword(string newWord)
		{
			//如果用户信息验证失败，则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				//新密码不能为空
				if (string.IsNullOrWhiteSpace(newWord)) AddBrokenRule(UserAccountFailureRules.NEW_PASSWORD_CANNOT_NULL);
			});

			string encryptPwd = newWord.PasswordEncrypt();

			//重置后的密码与重置前密码一致，则无须修改
			if (encryptPwd.Equals(UserInfo.Password, StringComparison.OrdinalIgnoreCase)) return true;

			return UsersAccessor.ResetPassword(ID, encryptPwd);
		}

		/// <summary>
		/// 设置为启用状态
		/// </summary>
		public bool SetEnabled()
		{
			//如果用户信息验证失败，则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				//不允许设置为启用时，抛出错误异常
				if (!CanSetEnabled()) AddBrokenRule(UserAccountFailureRules.CANNET_SET_ENABLED);
			});

			return UsersAccessor.SetStatus(ID, (int)UserStatus.ENABLED);
		}

		/// <summary>
		/// 设置为禁用状态
		/// </summary>
		public bool SetDisabled()
		{
			//如果用户信息验证失败，则抛出异常
			ThrowExceptionIfValidateFailure(() =>
			{
				//不允许设置为禁用时，抛出错误异常
				if (!CanSetDisabled()) AddBrokenRule(UserAccountFailureRules.CANNET_SET_DISABLED);
			});

			return UsersAccessor.SetStatus(ID, (int)UserStatus.DISABLED);
		}

		/// <summary>
		/// 修改资料
		/// </summary>
		/// <param name = "state" > 用户名 </param>
		/// <returns> </returns>
		public bool ModifyTo(UserModifyState state)
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				//编辑的内容为NULl
				if (state == null)
				{
					AddBrokenRule(UserAccountFailureRules.MODIFYSTATE_CANNOT_NULL);
				}
				else
				{
					// 用户名不能为空
					if (string.IsNullOrWhiteSpace(state.UserName)) AddBrokenRule(UserAccountFailureRules.USERNAME_REQUIRE);
					// 姓名不能为空
					else if (string.IsNullOrWhiteSpace(state.Name)) AddBrokenRule(UserAccountFailureRules.NAME_REQUIRE);
					// Email不能为空且格式要正确
					else if (!state.Email.IsEmail()) AddBrokenRule(UserAccountFailureRules.EMAIL_ERROR);
					// 手机号不能为空且格式要正确
					else if (!state.Mobile.IsCnPhone()) AddBrokenRule(UserAccountFailureRules.MOBILE_ERROR);
					// 用户名被使用
					else if (UsersAccessor.UsernameExists(state.UserName, ID)) AddBrokenRule(UserAccountFailureRules.USERNAME_EXISTS);
					// 邮箱被使用
					else if (UsersAccessor.EmailExists(state.Email, ID)) AddBrokenRule(UserAccountFailureRules.EMAIL_EXISTS);
					// 手机号被使用
					else if (UsersAccessor.MobileExists(state.Mobile, ID)) AddBrokenRule(UserAccountFailureRules.MOBILE_EXISTS);
				}
			});

			//权限编号序列组
			string permissionCodes = string.Empty;
			if (state.Permissions != null) permissionCodes = string.Join(",", state.Permissions);

			bool success = false;

			if (string.IsNullOrWhiteSpace(state.Password))
			{
				success = UsersAccessor.UpdateFor(ID, state.UserName, state.Email, state.Mobile, state.Title, state.Name, permissionCodes);
			}
			else
			{
				success = UsersAccessor.UpdateFor(ID, state.UserName, state.Email, state.Mobile, state.Title, state.Name, state.EncryptPassword, permissionCodes);
			}

			return success;
		}
	}
}