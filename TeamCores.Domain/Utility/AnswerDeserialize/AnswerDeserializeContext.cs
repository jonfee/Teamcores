using System;
using System.Collections.Generic;
using TeamCores.Domain.Enums;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility.AnswerDeserialize
{
	/// <summary>
	/// 题目答案项反序列化处理上下文
	/// </summary>
	public class AnswerDeserializeContext
	{
		/// <summary>
		/// 反序列化处理器
		/// </summary>
		private AnswerDeserializeHandler handler;

		/// <summary>
		/// 初始化一个<see cref="AnswerDeserializeContext"/>对象实例
		/// </summary>
		/// <param name="questionType">需要反序列化的答案项所属题目的类型(枚举：<see cref="QuestionType"/>)</param>
		public AnswerDeserializeContext(int questionType)
		{
			QuestionType type = (QuestionType)Enum.Parse(typeof(QuestionType), questionType.ToString());

			handler = CreateDeserializeHandler(type);
		}

		/// <summary>
		/// 初始化一个<see cref="AnswerDeserializeContext"/>对象实例
		/// </summary>
		/// <param name="questionType">需要反序列化的答案项所属题目的类型</param>
		public AnswerDeserializeContext(QuestionType questionType)
		{
			handler = CreateDeserializeHandler(questionType);
		}

		/// <summary>
		/// 根据题目类型，构建一个答案项反序化处理器
		/// </summary>
		/// <param name="questionType"></param>
		/// <returns></returns>
		private AnswerDeserializeHandler CreateDeserializeHandler(QuestionType questionType)
		{
			AnswerDeserializeHandler _handler = default(AnswerDeserializeHandler);

			switch (questionType)
			{
				case QuestionType.SINGLE_CHOICE:
					_handler = new SingleChoiceDeserializeHandler();
					break;
				case QuestionType.MULTIPLE_CHOICE:
					_handler = new MultipleChoiceDeserializeHandler();
					break;
				case QuestionType.TRUE_OR_FALSE:
					_handler = new TrueOrFalseDeserializeHandler();
					break;
				case QuestionType.GAP_FILLING:
					_handler = new GapFillingDeserializeHandler();
					break;
				case QuestionType.ESSAY_QUESTION:
					_handler = new EssayQuestionDeserializeHandler();
					break;
			}

			return _handler;
		}

		/// <summary>
		/// 反序列化
		/// </summary>
		/// <param name="serializeData"></param>
		/// <returns></returns>
		public IEnumerable<QuestionAnswer> Deserialize(string serializeData)
		{
			return handler.Deserialize(serializeData);
		}
	}
}
