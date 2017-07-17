using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Domain.Models
{
	/// <summary>
	/// 用户集合管理信息验证失败规则
	/// </summary>
	public enum UserManageFailureRules
	{
		/// <summary>
		/// 搜索器不能为空
		/// </summary>
		[Description("搜索器不能为空")]
		SEARCHER_CANNOT_NULL = 1,
		/// <summary>
		/// 页码不是有效范围值
		/// </summary>
		[Description("页码不是有效范围值")]
		PAGE_INDEX_OUTRANGE,
		/// <summary>
		/// 每页展示数不是有效范围值
		/// </summary>
		[Description("每页展示数不是有效范围值")]
		PAGE_SIZE_OUTRANGE,
	}

	/// <summary>
	/// 用户搜索器
	/// </summary>
	public class UserSearcher
	{
		public readonly string Keyword;

		public readonly int PageIndex;

		public readonly int PageSize;

		public UserSearcher(int pageIndex, int pageSize, string keyword)
		{
			this.Keyword = keyword;
			this.PageIndex = pageIndex;
			this.PageSize = pageSize;
		}
	}

	public class UserManage : EntityBase<long, UserManageFailureRules>
	{
		/// <summary>
		/// 搜索器
		/// </summary>
		private UserSearcher searcher;

		/// <summary>
		/// 总用户数（满足UserSearcher条件的）
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// 用户集合
		/// </summary>
		public List<UserAccount> Users { get; set; }

		public UserManage(UserSearcher searcher)
		{
			this.searcher = searcher;
		}

		/// <summary>
		/// 执行搜索
		/// </summary>
		public void Search()
		{
			PagerModel<Users> pager = new PagerModel<Users>();

			pager.Index = searcher.PageIndex;
			pager.Size = searcher.PageSize;

			UsersAccessor.Get(pager, searcher.Keyword);

			this.TotalCount = pager.Count;

			//转换数据
			this.Users = pager.Table.Select(p => new UserAccount(p)).ToList();
		}

		protected override void Validate()
		{
			if (searcher == null) this.AddBrokenRule(UserManageFailureRules.SEARCHER_CANNOT_NULL);

			if (searcher.PageIndex < 1) this.AddBrokenRule(UserManageFailureRules.PAGE_INDEX_OUTRANGE);

			if (searcher.PageSize < 1) this.AddBrokenRule(UserManageFailureRules.PAGE_SIZE_OUTRANGE);
		}

		/// <summary>
		/// 移除用户
		/// </summary>
		/// <param name="userId"></param>
		public void Remove(long userId)
		{
			var user = this.Users.Where(p => p.ID.Equals(userId)).FirstOrDefault();

			if (user != null)
			{
				//从当前集合中移除
				this.Users.Remove(user);

				//从数据库中移除
				user.ThrowExceptionIfValidateFailure();
				UsersAccessor.DeleteFor(userId);

				TotalCount = TotalCount - 1;
			}
		}
	}
}
