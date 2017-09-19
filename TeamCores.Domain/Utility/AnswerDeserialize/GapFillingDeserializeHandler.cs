using TeamCores.Common.Json;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility.AnswerDeserialize
{
	/// <summary>
	/// 填空题答案项反序化处理器
	/// </summary>
	public class GapFillingDeserializeHandler : AnswerDeserializeHandler
	{
		public override QuestionAnswer Deserialize(string serializeData)
		{
			if (string.IsNullOrWhiteSpace(serializeData)) return null;

			var type = new { Answer = string.Empty };

			var result = JsonUtility.DeserializeAnonymousType(serializeData, type);

			var answer = new GapFillingAnswer();
			answer.Answer = result.Answer;

			return answer;
		}
	}
}
