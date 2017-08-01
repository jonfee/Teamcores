using TeamCores.Models.Answer;

namespace TeamCores.Web.ViewModel.Question
{
    /// <summary>
    /// 题目信息编辑视图模型
    /// </summary>
    public class QuestionModifyViewModel
    {
        /// <summary>
        /// 题目ID
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// 归属课程ID
        /// </summary>
        public long CourseId { get; set; }

        /// <summary>
        /// 课程类型（单选，多选，对错，填空题，问答题）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 原始答案备选项
        /// </summary>
        public QuestionAnswer AnswerOptions { get; set; }

        /// <summary>
        /// 题目状态
        /// </summary>
        public int Status { get; set; }
    }
}
