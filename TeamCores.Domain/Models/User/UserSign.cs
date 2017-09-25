using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Events;

namespace TeamCores.Domain.Models.User
{
	internal enum UserSignFailureRules
	{
		/// <summary>
		/// 账号或密码错误
		/// </summary>
		[Description("账号或密码错误")]
		ACCOUNT_OR_PASSWORD_ERROR = 1,
		/// <summary>
		/// 账号被锁定
		/// </summary>
		[Description("账号被锁定，请联系管理员")]
		ACCOUNT_STATUS_DISABLED
	}

	internal class UserSign : EntityBase<long, UserSignFailureRules>
	{
		private string _account;
		private string _password;
		private Data.Entity.Users _user;
		/// <summary>
		/// 用户信息
		/// </summary>
		public Data.Entity.Users User
		{
			get
			{
				if (_user == null && !string.IsNullOrWhiteSpace(_account))
				{
					if (_account.Contains("@"))
					{
						_user = UsersAccessor.GetByEmail(_account);
					}
					else
					{
						_user = UsersAccessor.GetByMobile(_account);
					}

					if (_user != null) ID = _user.UserId;
				}

				return _user;
			}
		}

		public UserSign(string account, string password)
		{
			_account = account;
			_password = password;
		}

		/// <summary>
		/// 尝试登录
		/// </summary>
		/// <returns></returns>
		public bool TrySign()
		{
			ThrowExceptionIfValidateFailure();

			eventsChannels.AddEvent(new UserSignedEvent(new UserSignedEventState
			{
				UserId = ID,
				SignedTime = DateTime.Now,
				IP = string.Empty
			}));

			eventsChannels.Execute();

			return true;
		}

		protected override void Validate()
		{
			if (User == null)
			{
				AddBrokenRule(UserSignFailureRules.ACCOUNT_OR_PASSWORD_ERROR);
			}
			else
			{
				string encryptPwd = _password.PasswordEncrypt();

				if (encryptPwd != User.Password)
				{
					AddBrokenRule(UserSignFailureRules.ACCOUNT_OR_PASSWORD_ERROR);
				}
				else if(User.Status==(int)UserStatus.DISABLED)
				{
					AddBrokenRule(UserSignFailureRules.ACCOUNT_STATUS_DISABLED);
				}
			}
		}
	}
}
