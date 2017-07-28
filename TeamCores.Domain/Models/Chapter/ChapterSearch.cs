using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Models;

namespace TeamCores.Domain.Models.Chapter
{
	/// <summary>
	/// 课程章节验证错误结果枚举
	/// </summary>
	public enum ChapterSearchFailureRule
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
	/// 课程章节数据搜索领域模型
	/// </summary>
	public class ChapterSearch : EntityBase<long, ChapterSearchFailureRule>
	{
		#region 属性

		public long? CourseId { get; set; }

		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion

		#region 实例构造
		public ChapterSearch(int pageIndex, int pageSize, string keyword, long? courseId = null, int? status = null)
		{
			CourseId = courseId;
			Keyword = keyword;
			Status = status;
			PageIndex = pageIndex;
			PageSize = pageSize;
		}
		#endregion

		#region 验证

		protected override void Validate()
		{
			if (PageIndex < 1) this.AddBrokenRule(ChapterSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) this.AddBrokenRule(ChapterSearchFailureRule.PAGE_SIZE_OUTRANGE);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 执行搜索
		/// </summary>
		public PagerModel<Data.Entity.Chapter> Search()
		{
			this.ThrowExceptionIfValidateFailure();

			PagerModel<Data.Entity.Chapter> pager = new PagerModel<Data.Entity.Chapter>()
			{
				Index = PageIndex,
				Size = PageSize
			};

			ChapterAccessor.Get(pager, Keyword, courseId: CourseId, status: Status);

			return pager;
		}

		#endregion
	}
}
