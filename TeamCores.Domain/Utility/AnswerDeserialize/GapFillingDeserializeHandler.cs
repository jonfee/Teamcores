using System.Collections.Generic;
using TeamCores.Common.Json;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility.AnswerDeserialize
{
	/// <summary>
	/// 填空题答案项反序化处理器
	/// </summary>
	public class GapFillingDeserializeHandler : AnswerDeserializeHandler
	{
		public override IEnumerable<QuestionAnswer> Deserialize(string serializeData)
		{
			if (string.IsNullOrWhiteSpace(serializeData)) return null;

			return JsonUtility.JsonDeserialize<List<GapFillingAnswer>>(serializeData);
		}
	}
}
