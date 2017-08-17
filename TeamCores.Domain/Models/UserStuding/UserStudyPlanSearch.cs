using System.ComponentModel;
using TeamCores.Data.DataAccess;
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
		/// 学员ID
		/// </summary>
		public long? StudentId { get; set; }

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

		/// <summary>
		/// 初始化<see cref="UserStudyPlanSearch"/>对象实例
		/// </summary>
		/// <param name="pageIndex">当前页</param>
		/// <param name="pageSize">每页数</param>
		/// <param name="studentId">学员ID</param>
		/// <param name="studyStatus">学习状态,<see cref="Enums.UserStudyPlanStatus"/>枚举值,为NULL时不限制。</param>
		public UserStudyPlanSearch(int pageIndex, int pageSize, long? studentId, int? studyStatus)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			StudyStatus = studyStatus;
			StudentId = studentId;
		}

		#endregion

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
		public PagerModel<Data.Entity.UserStudyPlan> Search()
		{
			ThrowExceptionIfValidateFailure();

			//定义用户学习计划查询器
			PagerModel<Data.Entity.UserStudyPlan> pager = new PagerModel<Data.Entity.UserStudyPlan>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			//分页获取用户学习计划列表
			UserStudyPlanAccessor.Get(pager, studentId: StudentId, status: StudyStatus);

			return pager;
		}
		#endregion
	}
}
