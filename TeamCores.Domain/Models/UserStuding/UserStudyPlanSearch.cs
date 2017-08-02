using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Models.StudyPlan;
using TeamCores.Domain.Output;
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
		public UserStudyPlanSearch(int pageIndex, int pageSize, int? studyStatus)
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
		public PagerModel<UserStudyPlanSearchResultItem> Search()
		{
			ThrowExceptionIfValidateFailure();

			//定义用户学习计划查询器
			PagerModel<Data.Entity.UserStudyPlan> pager = new PagerModel<Data.Entity.UserStudyPlan>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			//分页获取用户学习计划列表
			UserStudyPlanAccessor.Get(pager, status: StudyStatus);

			var data = TransferData(pager.Table);

			return new PagerModel<UserStudyPlanSearchResultItem>
			{
				Index = pager.Index,
				Size = pager.Size,
				Count = pager.Count,
				Table = data
			};
		}

		/// <summary>
		/// 转换数据
		/// </summary>
		/// <param name="userPlans">用户学习计划情况集合</param>
		/// <returns></returns>
		private List<UserStudyPlanSearchResultItem> TransferData(IEnumerable<Data.Entity.UserStudyPlan> userPlans)
		{
			if (userPlans == null || userPlans.Count() < 1) return null;

			//用户学习计划ID集合
			var planIds = userPlans.Select(p => p.PlanId).ToArray();

			//获取所有的学习计划
			var plans = StudyPlanAccessor.GetList(planIds);

			if (plans == null || plans.Count() < 1) return null;

			var list = new List<UserStudyPlanSearchResultItem>();

			foreach (var plan in plans)
			{
				//用户学习计划执行情况
				var userPlan = userPlans.FirstOrDefault(p => p.PlanId == plan.PlanId);

				list.Add(new UserStudyPlanSearchResultItem
				{
					UserId = userPlan.UserId,
					PlanId = plan.PlanId,
					Title = plan.Title,
					Content = plan.Content,
					CreatorId = plan.UserId,
					StudentCount = plan.Student,
					PlanStatus = plan.Status,
					CreateTime = plan.CreateTime,
					StudyStatus = userPlan.Status,
					Progress = userPlan.Progress,
					LastStudyTime = userPlan.UpdateTime
				});
			}

			return list;
		}

		#endregion
	}
}
