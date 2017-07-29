using System;
using System.Collections.Generic;
using TeamCores.Common.Json;
using System.Linq;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Answer
{
    /// <summary>
    /// 单选题答案选项
    /// </summary>
    public class SingleChoiceAnswer : QuestionAnswer
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
            return type == (int)QuestionType.SINGLE_CHOICE;
        }

        public override string ToJson()
        {
            return JsonUtility.JsonSerializeObject(Options);
        }

        public override bool Validate()
        {
            int rightCount = Options.Count(p => p.IsRight);

            return Options.Count > 1 && rightCount == 1;
        }
    }
}
