using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Infrastructure.ExamPager
{
	/// <summary>
	/// 问答题构建状态
	/// </summary>
	internal class AskQuestionsBuildState : ExamPagerQuestionBuildState
	{
		public AskQuestionsBuildState(ExamPagerQuestionBuildContext context) : base(context)
		{
		}

		protected override bool CheckAnswers(Dictionary<string, string> answers)
		{
			return true;
		}

		protected override Dictionary<string, string> ConvertAnswerOptionsToDictionaryFor(string answersJson)
		{
			return default(Dictionary<string, string>);
		}

		protected override int CurrentStateMaxSize()
		{
			return Context.Exam.Ask;
		}

		protected override int CurrentStateTotalScore()
		{
			return Context.Exam.AskTotal;
		}

		protected override IEnumerable<Questions> GetCanUseQuestions()
		{
			if (Context.Exam.Radio < 1) return null;

			//可用且为单选的题库
			var data = Context.Questions.Where(p => p.Status == (int)QuestionStatus.ENABLED && p.Type == (int)QuestionType.ESSAY_QUESTION);

			return data;
		}

		protected override void NextStateRequest()
		{
			return;
		}
	}
}
