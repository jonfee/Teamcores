using System;
using TeamCores.Common.Json;

namespace TeamCores.Models.Answer
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

        public override string Serialize()
        {
            return JsonUtility.JsonSerializeObject(new { Answer });
        }

        public override bool Validate()
        {
            return true;
        }
    }
}
