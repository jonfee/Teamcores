using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Utility.AnswerDeserialize;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Utility
{
	/// <summary>
	/// 题目辅助工具类
	/// </summary>
	internal class QuestionTools
	{
		/// <summary>
		/// 检测指定的目类型是否需要人工阅卷
		/// </summary>
		/// <param name="type"><see cref="QuestionType" />枚举成员值</param>
		/// <returns></returns>
		public static bool HasMarking(int type)
		{
			QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), type.ToString());

			switch (questionType)
			{
				case QuestionType.SINGLE_CHOICE:
				case QuestionType.MULTIPLE_CHOICE:
				case QuestionType.TRUE_OR_FALSE:
					return false;
				case QuestionType.GAP_FILLING:
				case QuestionType.ESSAY_QUESTION:
				default:
					return true;
			}
		}

		/// <summary>
		/// 检测答案选项类型是否符合指定的题目类型
		/// </summary>
		/// <param name="answer"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool CheckAnswerOptionsType(QuestionAnswer answer, int type)
		{
			int answerWithType = 0;

			if (answer is SingleChoiceAnswer) answerWithType = (int)QuestionType.SINGLE_CHOICE;
			else if (answer is MultipleChoiceAnswer) answerWithType = (int)QuestionType.MULTIPLE_CHOICE;
			else if (answer is TrueFalseAnswer) answerWithType = (int)QuestionType.TRUE_OR_FALSE;
			else if (answer is GapFillingAnswer) answerWithType = (int)QuestionType.GAP_FILLING;
			else if (answer is EssayQuestionAnswer) answerWithType = (int)QuestionType.ESSAY_QUESTION;

			return answerWithType == type;
		}

		/// <summary>
		/// 将题目答案项反序化为对应的数据对象
		/// </summary>
		/// <param name="answers"></param>
		/// <param name="questionType"></param>
		/// <returns></returns>
		public static List<QuestionAnswer> DeserializeAnswers(string answers, int questionType)
		{
			var context = new AnswerDeserializeContext(questionType);

			return context.Deserialize(answers).ToList();
		}
	}
}
