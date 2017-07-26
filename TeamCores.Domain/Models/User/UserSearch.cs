using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Domain.Models.User
{
	/// <summary>
	/// 用户搜索验证失败规则
	/// </summary>
	public enum UserSearchFailureRules
	{
		/// <summary>
		/// 页码不是有效范围值
		/// </summary>
		[Description("页码不是有效范围值")]
		PAGE_INDEX_OUTRANGE = 1,
		/// <summary>
		/// 每页展示数不是有效范围值
		/// </summary>
		[Description("每页展示数不是有效范围值")]
		PAGE_SIZE_OUTRANGE,
	}

	public class UserSearch : EntityBase<long, UserSearchFailureRules>
	{
		#region 属性

		public readonly string Keyword;

		public readonly int? Status;

		public readonly int PageIndex;

		public readonly int PageSize;

		#endregion

		public UserSearch(int pageIndex, int pageSize, string keyword, int? status)
		{
			Keyword = keyword;
			Status = status;
			PageIndex = pageIndex;
			PageSize = pageSize;
		}

		protected override void Validate()
		{
			if (PageIndex < 1) this.AddBrokenRule(UserSearchFailureRules.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) this.AddBrokenRule(UserSearchFailureRules.PAGE_SIZE_OUTRANGE);
		}

		/// <summary>
		/// 执行搜索
		/// </summary>
		public PagerModel<Users> Search()
		{
			this.ThrowExceptionIfValidateFailure();

			PagerModel<Users> pager = new PagerModel<Users>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			UsersAccessor.Get(pager, Keyword, Status);

			return pager;
		}
	}
}
