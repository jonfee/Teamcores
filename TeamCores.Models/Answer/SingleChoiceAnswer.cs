using System;
using System.Collections.Generic;
using TeamCores.Common.Json;
using System.Linq;

namespace TeamCores.Models.Answer
{
    /// <summary>
    /// 单选题答案选项
    /// </summary>
    public class SingleChoiceAnswer : QuestionAnswer
    {
		/// <summary>
		/// 选项
		/// </summary>
		public List<AnswerChoiceOption> Options { get; set; }

		public SingleChoiceAnswer()
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

            return Options.Count > 1 && rightCount == 1;
        }
    }
}
