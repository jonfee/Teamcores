using TeamCores.Domain.Models.Answer;

namespace TeamCores.Web.ViewModel.Question
{
    /// <summary>
    /// 新题目视图模型
    /// </summary>
    public class NewQuestionViewModel
    {
        /// <summary>
        /// 归属课程ID
        /// </summary>
        public long CourseId { get; set; }

        /// <summary>
        /// 科目ID
        /// </summary>
        public long SubjectId { get; set; }

        /// <summary>
        /// 课程类型（单选，多选，对错，填空题，问答题）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否需要阅卷
        /// </summary>
        public bool Marking { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 原始答案备选项
        /// </summary>
        public QuestionAnswer AnswerOptions { get;set; }
    }
}
