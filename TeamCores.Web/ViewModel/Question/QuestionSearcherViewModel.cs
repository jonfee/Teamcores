using System.Collections.Generic;

namespace TeamCores.Web.ViewModel.Question
{
    /// <summary>
    /// 题目搜索视图模型
    /// </summary>
    public class QuestionSearcherViewModel
    {
        public long? CourseId { get; set; }

		public string questionIds { get; set; }

        public int? QuestionType { get; set; }

        public string Keyword { get; set; }

        public int? Status { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
