using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Services.Request;
using TeamCores.Models;

namespace TeamCores.Domain.Models.Exams
{
	internal enum ExamsSearchFailureRule
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

	internal class ExamsSearch : EntityBase<long, ExamsSearchFailureRule>
	{
		#region 属性

		/// <summary>
		/// 课程ID
		/// </summary>
		public long? CourseId { get; set; }

		/// <summary>
		/// 关键词
		/// </summary>
		public string Keyword { get; set; }

		/// <summary>
		/// 考卷状态
		/// </summary>
		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化一个<see cref="ExamsSearch"/>对象实例
		/// </summary>
		/// <param name="request"></param>
		public ExamsSearch(ExamsSearchRequest request)
		{
			if (request != null)
			{
				CourseId = request.CourseId;
				Keyword = request.Keyword;
				Status = request.Status;
				PageIndex = request.PageIndex;
				PageSize = request.PageSize;
			}
		}

		#endregion

		#region 验证
		protected override void Validate()
		{
			if (PageIndex < 1) this.AddBrokenRule(ExamsSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) this.AddBrokenRule(ExamsSearchFailureRule.PAGE_SIZE_OUTRANGE);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 执行搜索
		/// </summary>
		/// <returns></returns>
		public PagerModel<Data.Entity.Exams> Search()
		{
			ThrowExceptionIfValidateFailure();

			PagerModel<Data.Entity.Exams> pager = new PagerModel<Data.Entity.Exams>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			ExamsAccessor.GetList(pager, Keyword, courseId: CourseId, status: Status);

			return pager;
		}

		#endregion
	}
}
