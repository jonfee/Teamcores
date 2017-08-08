using System.Collections.Generic;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility.AnswerDeserialize
{
	/// <summary>
	/// 题目答案项反序列化处理器抽象基类
	/// </summary>
	public abstract class AnswerDeserializeHandler
	{
		/// <summary>
		/// 反序列化
		/// </summary>
		/// <param name="serializeData"></param>
		/// <returns></returns>
		public abstract IEnumerable<QuestionAnswer> Deserialize(string serializeData);
	}
}
