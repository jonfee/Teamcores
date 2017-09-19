using TeamCores.Common.Json;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility.AnswerDeserialize
{
	/// <summary>
	/// 问答题答案项（或知识点）反序列化处理器
	/// </summary>
	public class EssayQuestionDeserializeHandler : AnswerDeserializeHandler
	{
		public override QuestionAnswer Deserialize(string serializeData)
		{
			if (string.IsNullOrWhiteSpace(serializeData)) return null;

			var type = new { Knowledge = string.Empty };

			var result = JsonUtility.DeserializeAnonymousType(serializeData, type);
			
			var answer = new EssayQuestionAnswer();
			answer.Knowledge = result.Knowledge;

			return answer;
		}
	}
}
