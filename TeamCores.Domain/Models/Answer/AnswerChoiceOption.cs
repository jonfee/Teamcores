using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Models.Answer
{
    /// <summary>
    /// 答案选择项
    /// </summary>
    public class AnswerChoiceOption
    {
        /// <summary>
        /// 答案编号，如：A
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 答案内容
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 是否正确答案
        /// </summary>
        public bool IsRight { get; set; }
    }
}
