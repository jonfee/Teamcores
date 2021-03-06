using System.Collections.Generic;
using TeamCores.Common.Json;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility.AnswerDeserialize
{
	/// <summary>
	/// 单选题答案项反序列化处理器
	/// </summary>
	public class SingleChoiceDeserializeHandler : AnswerDeserializeHandler
	{
		public override QuestionAnswer Deserialize(string serializeData)
		{
			if (string.IsNullOrWhiteSpace(serializeData)) return null;

			var options= JsonUtility.JsonDeserialize<List<AnswerChoiceOption>>(serializeData);

			var answer = new SingleChoiceAnswer();
			answer.PushRange(options);

			return answer;
		}
	}
}
