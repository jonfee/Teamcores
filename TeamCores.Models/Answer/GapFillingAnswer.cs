using System;
using TeamCores.Common.Json;

namespace TeamCores.Models.Answer
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

        public override string Serialize()
        {
            return JsonUtility.JsonSerializeObject(new { Answer });
        }

        public override bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Answer);
        }
    }
}
