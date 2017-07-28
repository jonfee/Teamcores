using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Data.DataAccess;
using TeamCores.Models;

namespace TeamCores.Domain.Models.Course
{
	/// <summary>
	/// 课程搜索验证错误结果枚举
	/// </summary>
	public enum CourseSearchFailureRule
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

	public class CourseSearch : EntityBase<long, CourseSearchFailureRule>
	{
		#region 属性

		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion

		#region 初始化实例

		public CourseSearch(int pageIndex, int pageSize, string keyword, int? status)
		{
			Keyword = keyword;
			Status = status;
			PageIndex = pageIndex;
			PageSize = pageSize;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (PageIndex < 1) this.AddBrokenRule(CourseSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) this.AddBrokenRule(CourseSearchFailureRule.PAGE_SIZE_OUTRANGE);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 执行搜索
		/// </summary>
		public PagerModel<Data.Entity.Course> Search()
		{
			this.ThrowExceptionIfValidateFailure();

			PagerModel<Data.Entity.Course> pager = new PagerModel<Data.Entity.Course>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			CourseAccessor.Get(pager, Keyword, Status);

			return pager;
		}

		#endregion
	}
}
