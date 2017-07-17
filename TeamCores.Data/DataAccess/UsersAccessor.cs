﻿using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Data.DataAccess
{
	public class UsersAccessor
	{
		/// <summary>
		/// 编辑用户基本信息
		/// </summary>
		/// <param name="user"></param>
		public static void Edit(Users user)
		{
			using (var db = new DataContext())
			{
				db.Users.Attach(user);
				var entry = db.Entry(user);
				entry.Property(p => p.Email).IsModified = true;
				entry.Property(p => p.Username).IsModified = true;
				entry.Property(p => p.Password).IsModified = true;
				entry.Property(p => p.Mobile).IsModified = true;
				db.SaveChanges();
			}
		}

		/// <summary>
		/// 分页获取用户列表信息
		/// </summary>
		/// <param name="pager"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public static PagerModel<Users> Get(PagerModel<Users> pager, string keyword)
		{
			using (var db = new DataContext())
			{
				var query = from p in db.Users
							orderby p.CreateTime descending
							select p;
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					query = from p in query
							where p.Username.Contains(keyword) 
							|| p.Name.Contains(keyword) 
							|| p.Email.Contains(keyword) 
							|| p.Mobile.Contains(keyword)
							orderby p.CreateTime descending
							select p;
				}

				pager.Count = query.Count();
				var list = query.Skip((pager.Index - 1) * pager.Size).Take(pager.Size).ToList();
				pager.Table = list;
				return pager;
			}
		}

		/// <summary>
		/// 新增用户
		/// </summary>
		/// <param name="users"></param>
		/// <param name="profile"></param>
		/// <param name="study"></param>
		public static void Add(Users users, UserStudy study)
		{
			using (var db = new DataContext())
			{
				db.Users.Add(users);
				db.UserStudy.Add(study);
				db.SaveChanges();
			}
		}

		/// <summary>
		/// 根据用户ID，获取用户基本信息
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static Users Get(long userId)
		{
			using (var db = new DataContext())
			{
				return db.Users.SingleOrDefault(p => p.UserId == userId);
			}
		}

		/// <summary>
		/// 通过手机号码获取用户信息
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public static Users GetByMobile(string mobile)
		{
			using (var db = new DataContext())
			{
				return db.Users.SingleOrDefault(p => p.Mobile == mobile);
			}
		}

		/// <summary>
		/// 用户Email获取用户信息
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public static Users GetByEmail(string email)
		{
			using (var db = new DataContext())
			{
				return db.Users.SingleOrDefault(p => p.Email == email);
			}
		}

		/// <summary>
		/// 判断用户是否存在，true表示存在
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool UsernameExists(string name, long userid = 0)
		{
			using (var db = new DataContext())
			{
				return db.Users.Where(p => p.UserId != userid && p.Username == name).Count() > 0;
			}
		}


		/// <summary>
		/// 判断手机号码是否存在，true表示存在
		/// </summary>
		/// <param name="mobile"></param>
		/// <returns></returns>
		public static bool MobileExists(string mobile, long userid = 0)
		{
			using (var db = new DataContext())
			{
				return db.Users.Where(p => p.UserId != userid && p.Mobile == mobile).Count() > 0;
			}
		}

		/// <summary>
		/// 判断邮箱是否存在，true表示存在
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public static bool EmailExists(string email, long userid = 0)
		{
			using (var db = new DataContext())
			{
				return db.Users.Where(p => p.UserId != userid && p.Email == email).Count() > 0;
			}
		}

		public static long GetId(string name)
		{
			using (var db = new DataContext())
			{
				return db.Users.SingleOrDefault(p => p.Username == name).UserId;
			}
		}

		/// <summary>
		/// 重置用户登录密码
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="newWord">加密后的密码字符串</param>
		public static void ResetPassword(long userId, string newWord)
		{
			using (var db = new DataContext())
			{
				var user = db.Users.SingleOrDefault(p => p.UserId == userId);

				user.Password = newWord;

				db.Users.Update(user);

				db.SaveChanges();
			}
		}

		/// <summary>
		/// 修改资料
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="userName">用户名</param>
		/// <param name="email">邮箱</param>
		/// <param name="mobile">手机号</param>
		/// <param name="title">头衔</param>
		/// <param name="name">姓名</param>
		/// <returns></returns>
		public static void UpdateFor(long userId,string userName, string email, string mobile, string title, string name)
		{
			using (var db = new DataContext())
			{
				var user = db.Users.SingleOrDefault(p => p.UserId == userId);

				user.Username = userName;
				user.Email = email;
				user.Mobile = mobile;
				user.Title = title;
				user.Name = name;

				db.Users.Update(user);

				db.SaveChanges();
			}
		}

		/// <summary>
		/// 删除用户
		/// </summary>
		/// <param name="userId"></param>
		public static void DeleteFor(long userId)
		{
			using (var db = new DataContext())
			{
				var user = db.Users.SingleOrDefault(p => p.UserId == userId);

				user.Status = 0;

				db.Users.Update(user);

				db.SaveChanges();
			}
		}
	}
}
