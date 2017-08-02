﻿using System;
using System.ComponentModel;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models
{
    /// <summary>
    /// 用户账号信息验证失败规则
    /// </summary>
    internal enum UserAccountFailureRules
    {
        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")] USER_NOT_EXISTS = 1,

        /// <summary>
        /// 新密码不能为空
        /// </summary>
        [Description("新密码不能为空")] NEW_PASSWORD_CANNOT_NULL,

        /// <summary>
        /// 原密码验证失败
        /// </summary>
        [Description("原密码验证失败")] OLD_PASSWORD_CHECK_FAILURE,

        /// <summary>
        /// 用户名不能为空
        /// </summary>
        [Description("用户名不能为空")] USERNAME_REQUIRE,

        /// <summary>
        /// 用户名已被使用
        /// </summary>
        [Description("用户名已被使用")] USERNAME_EXISTS,

        /// <summary>
        /// 不是有效的电子邮箱地址
        /// </summary>
        [Description("不是有效的电子邮箱地址")] EMAIL_ERROR,

        /// <summary>
        /// 电子邮箱已被使用
        /// </summary>
        [Description("电子邮箱已被使用")] EMAIL_EXISTS,

        /// <summary>
        /// 不是有效的中国大陆手机号码
        /// </summary>
        [Description("不是有效的中国大陆手机号码")] MOBILE_ERROR,

        /// <summary>
        /// 手机号码已被使用
        /// </summary>
        [Description("手机号码已被使用")] MOBILE_EXISTS,

        /// <summary>
        /// 姓名不能为空
        /// </summary>
        [Description("姓名不能为空")] NAME_REQUIRE,

        /// <summary>
        /// 不能设置为启用
        /// </summary>
        [Description("不能设置为启用")] CANNET_SET_ENABLED,

        /// <summary>
        /// 不能设置为禁用
        /// </summary>
        [Description("不能设置为禁用")] CANNET_SET_DISABLED
    }

    /// <summary>
    /// 用户账号
    /// </summary>
    internal class UserAccount : EntityBase<long, UserAccountFailureRules>
    {
        /// <summary>
        /// 用户账号信息
        /// </summary>
        public Users UserInfo
        {
            get;
            private set;
        }

        public UserAccount(Users user)
        {
            if (user != null)
            {
                UserInfo = user;
                ID = user.UserId;
            }
        }

        public UserAccount(long userId)
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
            if (null != UserInfo && UserInfo.Status == (int) UserStatus.DISABLED)
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
            if (null != UserInfo && UserInfo.Status == (int) UserStatus.ENABLED)
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
        public void ModifyPassword(string oldWord, string newWord)
        {
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
                UserInfo.Password = newWord.PasswordEncrypt();

                UsersAccessor.ResetPassword(ID, UserInfo.Password);
            }
        }

        /// <summary>
        /// 重置用户登录密码
        /// </summary>
        /// <param name = "newWord" > </param>
        /// <returns> </returns>
        public void ResetPassword(string newWord)
        {
            //如果用户信息验证失败，则抛出异常
            ThrowExceptionIfValidateFailure(() =>
            {
                //新密码不能为空
                if (string.IsNullOrWhiteSpace(newWord)) AddBrokenRule(UserAccountFailureRules.NEW_PASSWORD_CANNOT_NULL);
            });

            UserInfo.Password = newWord.PasswordEncrypt();

            UsersAccessor.ResetPassword(ID, UserInfo.Password);
        }

        /// <summary>
        /// 设置为启用状态
        /// </summary>
        public void SetEnabled()
        {
            //如果用户信息验证失败，则抛出异常
            ThrowExceptionIfValidateFailure(() =>
            {
                //不允许设置为启用时，抛出错误异常
                if (!CanSetEnabled()) AddBrokenRule(UserAccountFailureRules.CANNET_SET_ENABLED);
            });

            UsersAccessor.SetStatus(ID, (int) UserStatus.ENABLED);
        }

        /// <summary>
        /// 设置为禁用状态
        /// </summary>
        public void SetDisabled()
        {
            //如果用户信息验证失败，则抛出异常
            ThrowExceptionIfValidateFailure(() =>
            {
                //不允许设置为禁用时，抛出错误异常
                if (!CanSetDisabled()) AddBrokenRule(UserAccountFailureRules.CANNET_SET_DISABLED);
            });

            UsersAccessor.SetStatus(ID, (int) UserStatus.DISABLED);
        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name = "userName" > 用户名 </param>
        /// <param name = "email" > 邮箱 </param>
        /// <param name = "mobile" > 手机号 </param>
        /// <param name = "title" > 头衔 </param>
        /// <param name = "name" > 姓名 </param>
        /// <returns> </returns>
        public void ModifyFor(string userName, string email, string mobile, string title, string name)
        {
            ThrowExceptionIfValidateFailure(() =>
            {
                // 用户名不能为空
                if (string.IsNullOrWhiteSpace(userName)) AddBrokenRule(UserAccountFailureRules.USERNAME_REQUIRE);

                // 姓名不能为空
                if (string.IsNullOrWhiteSpace(name)) AddBrokenRule(UserAccountFailureRules.NAME_REQUIRE);

                // Email不能为空且格式要正确
                if (!email.IsEmail()) AddBrokenRule(UserAccountFailureRules.EMAIL_ERROR);

                // 手机号不能为空且格式要正确
                if (!mobile.IsCnPhone()) AddBrokenRule(UserAccountFailureRules.MOBILE_ERROR);

                // 用户名被使用
                if (UsersAccessor.UsernameExists(userName, ID)) AddBrokenRule(UserAccountFailureRules.USERNAME_EXISTS);

                // 邮箱被使用
                if (UsersAccessor.EmailExists(email, ID)) AddBrokenRule(UserAccountFailureRules.EMAIL_EXISTS);

                // 手机号被使用
                if (UsersAccessor.MobileExists(mobile, ID)) AddBrokenRule(UserAccountFailureRules.MOBILE_EXISTS);
            });

            UsersAccessor.UpdateFor(ID, userName, email, mobile, title, name);
        }
    }
}