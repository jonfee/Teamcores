using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TeamCores.Common;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Request;

namespace TeamCores.Domain.Models.User
{
	/// <summary>
	/// 新用户业务验证规则结果
	/// </summary>
	internal enum NewUserFailureRules
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
		NAME_REQUIRE,
		/// <summary>
		/// 权限未设置
		/// </summary>
		[Description("权限未设置")]
		PERMISSIONS_NOSET
	}

	/// <summary>
	/// 用户帐户领域对象
	/// </summary>
	internal class NewUser : EntityBase<long, NewUserFailureRules>
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

		/// <summary>
		/// 状态
		/// </summary>
		public int Status => (int)UserStatus.ENABLED;

		/// <summary>
		/// 权限集合
		/// </summary>
		public string[] Permissions { get; set; }

		/// <summary>
		/// 是否忽略权限
		/// </summary>
		public bool IgnorePermission { get; set; }

		/// <summary>
		/// 学习情况
		/// </summary>
		public readonly UserStudy Study;

		#endregion

		public NewUser(NewUserRequest request)
		{
			ID = IDProvider.NewId;
			if (request != null)
			{
				Username = request.Username;
				Email = request.Email;
				Mobile = request.Mobile;
				Password = request.Password;
				Name = request.Name;
				Title = request.Title;
				Permissions = request.Permissions;
				IgnorePermission = request.IgnorePermission;
			}

			Study = new UserStudy
			{
				UserId = this.ID,
				Answers = 0,
				Average = 0,
				ReadCount = 0,
				StudyPlans = 0,
				StudyTimes = 0,
				TestExams = 0
			};
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
			else if (string.IsNullOrWhiteSpace(Password)) AddBrokenRule(NewUserFailureRules.PASSWORD_REQUIRE);
			// 姓名不能为空
			else if (string.IsNullOrWhiteSpace(Name)) AddBrokenRule(NewUserFailureRules.NAME_REQUIRE);
			// Email不能为空且格式要正确
			else if (!Email.IsEmail()) AddBrokenRule(NewUserFailureRules.EMAIL_ERROR);
			// 手机号不能为空且格式要正确
			else if (!Mobile.IsCnPhone()) AddBrokenRule(NewUserFailureRules.MOBILE_ERROR);
			// 用户名被使用
			else if (CheckForUsername()) AddBrokenRule(NewUserFailureRules.USERNAME_EXISTS);
			// 邮箱被使用
			else if (CheckForEmail()) AddBrokenRule(NewUserFailureRules.EMAIL_EXISTS);
			// 手机号被使用
			else if (CheckForMobile()) AddBrokenRule(NewUserFailureRules.MOBILE_EXISTS);
			//未忽略权限，但权限未设置
			else if (!IgnorePermission && (Permissions == null || Permissions.Length < 1)) AddBrokenRule(NewUserFailureRules.PERMISSIONS_NOSET);
		}

		#endregion

		#region 操作方法

		public bool Save()
		{
			ThrowExceptionIfValidateFailure();

			string permissionCodes = string.Empty;
			if (Permissions != null) permissionCodes = string.Join("", Permissions);

			//新用户仓储对象
			Users user = new Users
			{
				UserId = ID,
				Username = Username,
				Name = Name,
				Password = EncryptPassword,
				Title = Title,
				Email = Email,
				Mobile = Mobile,
				CreateTime = DateTime.Now,
				LastTime = DateTime.Now,
				LoginCount = 0,
				Status = Status,
				PermissionCode = permissionCodes
			};

			return UsersAccessor.Add(user, Study);
		}

		#endregion
	}
}
