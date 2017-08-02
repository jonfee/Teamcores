using System;
using TeamCores.Common.Exceptions;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models.User;
using TeamCores.Domain.Services.Request;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 用户相关服务
	/// </summary>
    public class UserService
	{
		/// <summary>
		/// 新增用户，同时初始化用户的学习情况数据
		/// </summary>
		/// <param name="newUser"></param>
		public void AddUser(NewUserRequest request)
		{
            NewUser newUser = new NewUser(
                request.Username,
                request.Email,
                request.Mobile,
                request.Password,
                request.Name,
                request.Title);

            newUser.Save();
		}

		/// <summary>
		/// 修改用户登录密码
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="oldWord"></param>
		/// <param name="newWord"></param>
		/// <returns></returns>
		public void ModifyPassword(long userId, string oldWord, string newWord)
		{
			UserAccount user = new UserAccount(userId);

			user.ModifyPassword(oldWord, newWord);
		}

		/// <summary>
		/// 重置用户登录密码
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="newWord"></param>
		/// <returns></returns>
		public void ResetPassword(long userId, string newWord)
		{
			UserAccount user = new UserAccount(userId);

			user.ResetPassword(newWord);
		}

		/// <summary>
		/// 修改资料
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="userName">用户名</param>
		/// <param name="email">邮箱</param>
		/// <param name="mobile">手机号</param>
		/// <param name="title">头衔</param>
		/// <param name="name">姓名</param>
		/// <returns></returns>
		public void ModifyFor(long userId, string userName, string email, string mobile, string title, string name)
		{
			UserAccount user = new UserAccount(userId);

			user.ModifyFor(userName, email, mobile, title, name);
		}

		/// <summary>
		/// 设置用户状态为启用
		/// </summary>
		/// <param name="userId"></param>
		public void SetEnabled(long userId)
		{
		    UserAccount user = new UserAccount(userId);

			user.SetEnabled();
		}

		/// <summary>
		/// 设置用户状态为禁用
		/// </summary>
		/// <param name="userId"></param>
		public void SetDisabled(long userId)
		{
			UserAccount user = new UserAccount(userId);

			user.SetDisabled();
		}

		/// <summary>
		/// 搜索用户信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="keyword"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public PagerModel<Users> Search(int pageSize, int pageIndex, string keyword, int? status)
		{
			UserSearch search = new UserSearch(pageIndex, pageSize, keyword, status);

			return search.Search();
		}
	}
}
