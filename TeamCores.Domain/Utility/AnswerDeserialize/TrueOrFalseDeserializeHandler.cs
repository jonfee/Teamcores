using System.Collections.Generic;
using TeamCores.Common.Json;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility.AnswerDeserialize
{
	/// <summary>
	/// 判断题答案项反序列化处理器
	/// </summary>
	public class TrueOrFalseDeserializeHandler : AnswerDeserializeHandler
	{
		public override QuestionAnswer Deserialize(string serializeData)
		{
			if (string.IsNullOrWhiteSpace(serializeData)) return null;

			return JsonUtility.JsonDeserialize<TrueFalseAnswer>(serializeData);
		}
	}
}
