using System;
using TeamCores.Common.Json;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Answer
{
    /// <summary>
    /// 填空题答案项
    /// </summary>
    public class GapFillingAnswer : QuestionAnswer
    {
        /// <summary>
        /// 正确答案
        /// </summary>
        public string Answer { get; set; }

        public override bool RegexType(int type)
        {
            return type == (int)QuestionType.GAP_FILLING;
        }

        public override string ToJson()
        {
            return JsonUtility.JsonSerializeObject(new { Answer });
        }

        public override bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Answer);
        }
    }
}
