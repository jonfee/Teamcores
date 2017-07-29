using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Models;

namespace TeamCores.Domain.Models.Subject
{
    /// <summary>
    /// 科目搜索验证错误结果枚举
    /// </summary>
    internal enum SubjectSearchFailureRule
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

    internal class SubjectSearch : EntityBase<long, SubjectSearchFailureRule>
	{
		#region 属性

		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion

		public SubjectSearch(int pageIndex, int pageSize, string keyword, int? status)
		{
			Keyword = keyword;
			Status = status;
			PageIndex = pageIndex;
			PageSize = pageSize;
		}

		protected override void Validate()
		{
			if (PageIndex < 1) this.AddBrokenRule(SubjectSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) this.AddBrokenRule(SubjectSearchFailureRule.PAGE_SIZE_OUTRANGE);
		}

		/// <summary>
		/// 执行搜索
		/// </summary>
		public PagerModel<Subjects> Search()
		{
			this.ThrowExceptionIfValidateFailure();

			PagerModel<Subjects> pager = new PagerModel<Subjects>()
			{
				Index = PageIndex,
				Size = PageSize
			};
			
			SubjectsAccessor.Get(pager, Keyword, Status);

			return pager;
		}
	}
}
