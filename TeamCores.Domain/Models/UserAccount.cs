using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;

namespace TeamCores.Domain.Models
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
        MOBILE_EXISTS
    }

    /// <summary>
    /// 用户账号
    /// </summary>
    public class UserAccount : EntityBase<long, UserAccountFailureRules>
    {
        /// <summary>
        /// 用户账号信息
        /// </summary>
        public Users UserInfo { get; private set; }

        public UserAccount(long userId)
        {
            this.ID = userId;

            InitUser();
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUser()
        {
            UserInfo = UsersAccessor.Get(this.ID);
        }

        protected override void Validate()
        {
            if (UserInfo == null) AddBrokenRule(UserAccountFailureRules.USER_NOT_EXISTS);
        }

        public bool ModifyPassword(string oldWord, string newWord)
        {
            return false;
        }

        public bool ResetPassword(string newWord)
        {
            return false;
        }

        public bool Modify(string userName,string email,string mobile,string title,string name)
        {
            return false;
        }
    }
}
