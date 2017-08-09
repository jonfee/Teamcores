using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Infrastructure.ExamPager
{
	/// <summary>
	/// 判断题构建状态
	/// </summary>
	internal class TrueOrFalseQuestionsBuildState : ExamPagerQuestionBuildState
	{
		public TrueOrFalseQuestionsBuildState(ExamPagerQuestionBuildContext context) : base(context)
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
			return Context.Exam.Judge;
		}

		protected override int CurrentStateTotalScore()
		{
			return Context.Exam.JudgeTotal;
		}

		protected override IEnumerable<Questions> GetCanUseQuestions()
		{
			if (Context.Exam.Judge < 1) return null;

			//可用且为单选的题库
			var data = Context.Questions.Where(p => p.Status == (int)QuestionStatus.ENABLED && p.Type == (int)QuestionType.TRUE_OR_FALSE);

			return data;
		}

		protected override void NextStateRequest()
		{
			Context.SetState(new GapFillQuestionsBuildState(Context));

			Context.BuildQuestions();
		}
	}
}
