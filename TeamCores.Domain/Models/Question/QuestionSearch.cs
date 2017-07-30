using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Models;

namespace TeamCores.Domain.Models.Question
{
    /// <summary>
    /// 题目搜索验证错误结果枚举
    /// </summary>
    internal enum QuestionSearchFailureRule
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

    internal class QuestionSearch : EntityBase<long, QuestionSearchFailureRule>
    {
        #region 属性

        public long? CourseId { get; set; }

        public int? QuestionType { get; set; }

        public string Keyword { get; set; }

        public int? Status { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        #endregion

        #region 实例化构造函数

        public QuestionSearch(int pageIndex, int pageSize, string keyword,int? questionType, long? courseId = null, int? status = null)
        {
            CourseId = courseId;
            Keyword = keyword;
            QuestionType = questionType;
            Status = status;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        #endregion

        #region 验证

        protected override void Validate()
        {
            if (PageIndex < 1) AddBrokenRule(QuestionSearchFailureRule.PAGE_INDEX_OUTRANGE);

            if (PageSize < 1) AddBrokenRule(QuestionSearchFailureRule.PAGE_SIZE_OUTRANGE);
        }

        #endregion

        #region 操作方法

        /// <summary>
		/// 执行搜索
		/// </summary>
		public PagerModel<Data.Entity.Questions> Search()
        {
            ThrowExceptionIfValidateFailure();

            PagerModel<Data.Entity.Questions> pager = new PagerModel<Data.Entity.Questions>()
            {
                Index = PageIndex,
                Size = PageSize
            };

            QuestionsAccessor.Get(pager, Keyword, type: QuestionType, courseId: CourseId, status: Status);

            return pager;
        }

        #endregion
    }
}
