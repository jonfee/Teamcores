using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Common;
using TeamCores.Common.Exceptions;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	public class UserService
	{
		/// <summary>
		/// 新增用户，同时初始化用户的学习情况数据
		/// </summary>
		/// <param name="newUser"></param>
		public void AddUser(NewUser newUser)
		{
			//对象为null时，抛出业务异常
			if (newUser == null) throw new UserNullException(nameof(newUser), "新增的用户对象不能为NULL。");

			//校验领域对象是否存在错误的规则
			newUser.ThrowExceptionIfValidateFailure();

			//新用户仓储对象
			Users user = new Users
			{
				UserId = newUser.ID,
				Username = newUser.Username,
				Name = newUser.Name,
				Password = newUser.EncryptPassword,
				Title = newUser.Title,
				Email = newUser.Email,
				Mobile = newUser.Mobile,
				CreateTime = DateTime.Now,
				LastTime = DateTime.Now,
				LoginCount = 0,
				Status = 1	//1表示启用
			};

			//初始化新用户的学习情况
			UserStudy study = new UserStudy
			{
				UserId = user.UserId,
				Answers = 0,
				Average = 0,
				ReadCount = 0,
				StudyPlans = 0,
				StudyTimes = 0,
				TestExams = 0
			};

			UsersAccessor.Add(user, study);
		}

		/// <summary>
		/// 删除用户
		/// </summary>
		/// <param name="userId"></param>
		public void DeleteUser(long userId)
		{
			UserAccount user = new UserAccount(userId);

			user.ThrowExceptionIfValidateFailure();

			UsersAccessor.DeleteFor(userId);
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
		/// 搜索用户信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="keyword"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public PagerModel<Users> Search(int pageSize, int pageIndex, string keyword, int? status)
		{
			UserSearcher searcher = new UserSearcher(pageIndex, pageSize, keyword, status);

			UserManage manage = new UserManage(searcher);

			return manage.Search();
		}
	}
}
