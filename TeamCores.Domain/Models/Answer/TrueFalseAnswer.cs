using System;
using TeamCores.Common.Json;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Answer
{
    /// <summary>
    /// 判断题答案项
    /// </summary>
    public class TrueFalseAnswer : QuestionAnswer
    {
        /// <summary>
        /// 答案
        /// </summary>
        public bool Answer { get; set; }

        public override bool RegexType(int type)
        {
            return type == (int)QuestionType.TRUE_OR_FALSE;
        }

        public override string ToJson()
        {
            return JsonUtility.JsonSerializeObject(new { Answer });
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
