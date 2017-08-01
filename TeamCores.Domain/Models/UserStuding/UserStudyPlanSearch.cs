using System.ComponentModel;
using TeamCores.Models;

namespace TeamCores.Domain.Models.UserStuding
{
	/// <summary>
	/// 用户学习计划搜索验证错误结果枚举
	/// </summary>
	internal enum UserStudyPlanSearchFailureRule
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

	/// <summary>
	/// 用户学习计划搜索业务领域模型
	/// </summary>
	internal class UserStudyPlanSearch : EntityBase<long, UserStudyPlanSearchFailureRule>
	{
		#region 属性

		/// <summary>
		/// 学习状态
		/// </summary>
		public int? StudyStatus { get; set; }

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

		#endregion

		/// <summary>
		/// 初始化<see cref="UserStudyPlanSearch"/>对象实例
		/// </summary>
		/// <param name="pageIndex">当前页</param>
		/// <param name="pageSize">每页数</param>
		/// <param name="studyStatus">学习状态,<see cref="Enums.UserStudyPlanStatus"/>枚举值,为NULL时不限制。</param>
		public UserStudyPlanSearch(int pageIndex,int pageSize,int? studyStatus)
		{
			PageIndex = pageIndex;
			PageSize = PageSize;
			StudyStatus = studyStatus;
		}

		#region 验证

		protected override void Validate()
		{
			if (PageIndex < 1) this.AddBrokenRule(UserStudyPlanSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) this.AddBrokenRule(UserStudyPlanSearchFailureRule.PAGE_SIZE_OUTRANGE);
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

			//StudyPlanAccessor.Get(pager, Keyword, status: Status);

			return pager;
		}

		#endregion
	}
}
