using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Infrastructure.ExamPager
{
	/// <summary>
	/// 填空题构建状态
	/// </summary>
	internal class GapFillQuestionsBuildState : ExamPagerQuestionBuildState
	{
		public GapFillQuestionsBuildState(ExamPagerQuestionBuildContext context) : base(context)
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
			return Context.Exam.Filling;
		}

		protected override int CurrentStateTotalScore()
		{
			return Context.Exam.FillingTotal;
		}

		protected override IEnumerable<Questions> GetCanUseQuestions()
		{
			if (Context.Exam.Filling < 1) return null;

			//可用且为单选的题库
			var data = Context.Questions.Where(p => p.Status == (int)QuestionStatus.ENABLED && p.Type == (int)QuestionType.GAP_FILLING);

			return data;
		}

		protected override void NextStateRequest()
		{
			Context.SetState(new AskQuestionsBuildState(Context));

			Context.BuildQuestions();
		}
	}
}
