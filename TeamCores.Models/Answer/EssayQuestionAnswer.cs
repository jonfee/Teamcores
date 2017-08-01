using System;
using TeamCores.Common.Json;

namespace TeamCores.Models.Answer
{
    /// <summary>
    /// 问答题（主观题）
    /// </summary>
    public class EssayQuestionAnswer : QuestionAnswer
    {
        /// <summary>
        /// 知识点
        /// </summary>
        public string Knowledge { get; set; }

        public override string ToJson()
        {
            return JsonUtility.JsonSerializeObject(new { Knowledge });
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
