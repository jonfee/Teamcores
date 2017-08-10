using System.Collections.Generic;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Infrastructure.ExamPager
{
	/// <summary>
	/// 学员考试卷题目生成上下文
	/// </summary>
	internal class ExamPagerQuestionBuildContext
	{
		#region 变量

		/// <summary>
		/// 本试卷的题目集合
		/// </summary>
		private List<ExamPaperQuestion> currentQuestions;
		
		private ExamPagerQuestionBuildState state;
		/// <summary>
		/// 题目构建状态
		/// </summary>
		public ExamPagerQuestionBuildState State
		{
			get
			{
				return state;
			}
		}

		/// <summary>
		/// 下一个题的排序编号
		/// </summary>
		private int nextSort;

		#endregion

		#region  属性

		/// <summary>
		/// 本试卷的考卷模板信息
		/// </summary>
		public readonly Data.Entity.Exams Exam;

		/// <summary>
		/// 题库数据
		/// </summary>
		public readonly IEnumerable<Data.Entity.Questions> Questions;

		#endregion

		/// <summary>
		/// 初始化一个<see cref="ExamPagerQuestionBuildContext"/>对象实例
		/// </summary>
		/// <param name="exam"></param>
		/// <param name="questions"></param>
		public ExamPagerQuestionBuildContext(Data.Entity.Exams exam, IEnumerable<Data.Entity.Questions> questions)
		{
			Exam = exam;
			Questions = questions;
			currentQuestions = new List<ExamPaperQuestion>();
			nextSort = 1;
		}

		/// <summary>
		/// 添加一个题目到新试卷
		/// </summary>
		/// <param name="question"></param>
		public void AddQuestion(ExamPaperQuestion question)
		{
			currentQuestions.Add(question);

			nextSort++;
		}

		/// <summary>
		/// 设置/变更状态
		/// </summary>
		/// <param name="state"></param>
		public void SetState(ExamPagerQuestionBuildState state)
		{
			this.state = state;
		}

		/// <summary>
		/// 构建本试卷的题目
		/// </summary>
		/// <returns></returns>
		public List<ExamPaperQuestion> BuildQuestions()
		{
			State.Build();

			return currentQuestions;
		}

		/// <summary>
		/// 检测题目是否已被选入在本试卷中
		/// </summary>
		/// <param name="questionId">题目ID</param>
		/// <returns></returns>
		public bool ExistsInExamPagerFor(long questionId)
		{
			return currentQuestions.Exists(p => p.QuestionId == questionId);
		}

		/// <summary>
		/// 获取下一题目的排序编号
		/// </summary>
		/// <returns></returns>
		public int GetNextSort()
		{
			return nextSort;
		}
	}
}
