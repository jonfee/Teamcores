using System;
using System.Collections.Generic;
using TeamCores.Common.Json;
using System.Linq;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Answer
{
    /// <summary>
    /// 多选题答案选项
    /// </summary>
    public class MultipleChoiceAnswer : QuestionAnswer
    {
        /// <summary>
        /// 选项
        /// </summary>
        public List<AnswerChoiceOption> Options = new List<AnswerChoiceOption>();

        public void Push(AnswerChoiceOption option)
        {
            Options.Add(option);
        }

        public override bool RegexType(int type)
        {
            return type == (int)QuestionType.MULTIPLE_CHOICE;
        }

        public override string ToJson()
        {
            return JsonUtility.JsonSerializeObject(Options);
        }

        public override bool Validate()
        {
            int rightCount = Options.Count(p => p.IsRight);

            return Options.Count > 2 && rightCount > 1;
        }
    }
}
