using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Utility;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Infrastructure.ExamPager
{
	/// <summary>
	/// 多选题构建状态
	/// </summary>
	internal class MultipleChoiceQuestionsBuildState : ExamPagerQuestionBuildState
	{
		public MultipleChoiceQuestionsBuildState(ExamPagerQuestionBuildContext context) : base(context)
		{
		}

		protected override bool CheckAnswers(Dictionary<string, string> answers)
		{
			return answers != null && answers.Count() > 2;
		}

		protected override Dictionary<string, string> ConvertAnswerOptionsToDictionaryFor(string answersJson)
		{
			//答案选项集合
			var answerDic = new Dictionary<string, string>();

			//从答案选项字符串中反序列化成答案对象
			var answers = QuestionTools.DeserializeAnswers(answersJson, (int)QuestionType.MULTIPLE_CHOICE) as MultipleChoiceAnswer;
			if (answers != null && answers.Options != null && answers.Options.Count() > 0)
			{
				answerDic = answers.Options.ToDictionary(k => k.Code, v => v.Answer);
			}

			return answerDic;
		}

		protected override int CurrentStateMaxSize()
		{
			return Context.Exam.Multiple;
		}

		protected override int CurrentStateTotalScore()
		{
			return Context.Exam.MultipleTotal;
		}

		protected override IEnumerable<Questions> GetCanUseQuestions()
		{
			if (Context.Exam.Multiple < 1) return null;

			//可用且为单选的题库
			var data = Context.Questions.Where(p => p.Status == (int)QuestionStatus.ENABLED && p.Type == (int)QuestionType.MULTIPLE_CHOICE);

			return data;
		}

		protected override void NextStateRequest()
		{
			Context.SetState(new TrueOrFalseQuestionsBuildState(Context));

			Context.BuildQuestions();
		}
	}
}
