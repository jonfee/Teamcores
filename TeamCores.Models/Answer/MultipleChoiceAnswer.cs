using System;
using System.Collections.Generic;
using TeamCores.Common.Json;
using System.Linq;

namespace TeamCores.Models.Answer
{
    /// <summary>
    /// 多选题答案选项
    /// </summary>
    public class MultipleChoiceAnswer : QuestionAnswer
    {
        /// <summary>
        /// 选项
        /// </summary>
        public List<AnswerChoiceOption> Options { get; set; }

		public MultipleChoiceAnswer()
		{
			Options = new List<AnswerChoiceOption>();
		}

        public void Push(AnswerChoiceOption option)
        {
            Options.Add(option);
        }

		public void PushRange(IEnumerable<AnswerChoiceOption> options)
		{
			Options.AddRange(options);
		}

		public override string Serialize()
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
