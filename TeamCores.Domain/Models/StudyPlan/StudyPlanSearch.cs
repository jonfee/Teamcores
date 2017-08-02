using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Models;

namespace TeamCores.Domain.Models.StudyPlan
{
	/// <summary>
	/// 学习计划搜索验证错误结果枚举
	/// </summary>
	internal enum StudyPlanSearchFailureRule
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

	internal class StudyPlanSearch : EntityBase<long, StudyPlanSearchFailureRule>
	{
		#region 属性

		/// <summary>
		/// 学习计划关键词
		/// </summary>
		public string Keyword { get; set; }

		/// <summary>
		/// 计划状态
		/// </summary>
		public int? Status { get; set; }

		/// <summary>
		/// 当前页
		/// </summary>
		public int PageIndex { get; set; }

		/// <summary>
		/// 每页数量
		/// </summary>
		public int PageSize { get; set; }

		#endregion

		#region 构造函数

		public StudyPlanSearch(int pageIndex, int pageSize, string keyword, int? status = null)
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
			if (PageIndex < 1) this.AddBrokenRule(StudyPlanSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) this.AddBrokenRule(StudyPlanSearchFailureRule.PAGE_SIZE_OUTRANGE);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 执行搜索
		/// </summary>
		public PagerModel<Data.Entity.StudyPlan> Search()
		{
			this.ThrowExceptionIfValidateFailure();

			PagerModel<Data.Entity.StudyPlan> pager = new PagerModel<Data.Entity.StudyPlan>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			StudyPlanAccessor.Get(pager, Keyword, status: Status);

			return pager;
		}

		#endregion
	}
}
