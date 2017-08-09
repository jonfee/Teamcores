using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamCores.Domain.Infrastructure.ExamPager
{
	/// <summary>
	/// 学员考试卷题目生成状态
	/// </summary>
	internal abstract class ExamPagerQuestionBuildState
	{
		/// <summary>
		/// 学员考试卷题目构建上下文对象
		/// </summary>
		protected readonly ExamPagerQuestionBuildContext Context;

		public ExamPagerQuestionBuildState(ExamPagerQuestionBuildContext context)
		{
			Context = context;
		}

		/// <summary>
		/// 构建题目
		/// </summary>
		public void Build()
		{
			if (Context == null) return;
			if (Context.Exam == null) return;
			if (Context.Questions == null) return;

			//获取当前状态下所有可用的题目
			var stateAllQuestions = GetCanUseQuestions();

			if (stateAllQuestions != null && stateAllQuestions.Count() > 0)
			{
				//当前状态下的题型最多允许的题目数量
				int maxSize = CurrentStateMaxSize();
				//可用的单选题目数
				var questionsCount = stateAllQuestions.Count();
				if (questionsCount < maxSize) maxSize = questionsCount;

				//当前状态下的题型总分
				var totalScore = CurrentStateTotalScore();
				//每题的平均分值
				int avgScore = (int)Math.Floor((double)totalScore / maxSize);
				//剩余分值
				int modScore = totalScore - maxSize * avgScore;

				Random random = null;

				while (maxSize > 0)
				{
					//随机种子实例
					random = new Random(Guid.NewGuid().GetHashCode());

					//随机出的题目索引
					var index = random.Next(questionsCount);

					//随机出的题目
					var item = stateAllQuestions.ElementAt(index);

					//题目存在，且不在本试卷中才可继续处理
					if (item != null && !Context.ExistsInExamPagerFor(item.QuestionId))
					{
						//本题的分值：如果为最后一题则为平均分值+未除完的分值，否则为平均分值，
						var score = maxSize > 1 ? avgScore : avgScore + modScore;

						//答案选项集合
						var answerDic = ConvertAnswerOptionsToDictionaryFor(item.Answer);

						if (CheckAnswers(answerDic))
						{
							//将题目加入到本试卷
							Context.AddQuestion(new Services.Response.ExamPaperQuestion
							{
								QuestionId = item.QuestionId,
								Score = score,
								Topic = item.Topic,
								SortCode = Context.GetNextSort(),
								Type = item.Type,
								Answers = answerDic
							});

							maxSize--;
						}
					}
				}
			}

			//执行下一状态请求
			NextStateRequest();
		}

		/// <summary>
		/// 当前状态下可使用的题目
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected abstract IEnumerable<Data.Entity.Questions> GetCanUseQuestions();

		/// <summary>
		/// 获取当前状态下最多允许的题目数量
		/// </summary>
		/// <returns></returns>
		protected abstract int CurrentStateMaxSize();

		/// <summary>
		/// 获取当前状态下的题型总分
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected abstract int CurrentStateTotalScore();

		/// <summary>
		/// 将题目的答案转换为<see cref="Dictionary{TKey, TValue}"/>集合，其中TKey表示答案编号（如：A），TValue表示答案内容
		/// </summary>
		/// <param name="answers"></param>
		/// <returns></returns>
		protected abstract Dictionary<string, string> ConvertAnswerOptionsToDictionaryFor(string answersJson);

		/// <summary>
		/// 检测题目的答案项是否有效
		/// </summary>
		/// <param name="answers"><see cref="Dictionary{TKey, TValue}"/>集合，其中TKey表示答案编号（如：A），TValue表示答案内容</param>
		/// <returns></returns>
		protected abstract bool CheckAnswers(Dictionary<string, string> answers);

		/// <summary>
		/// 下一状态请求
		/// </summary>
		protected abstract void NextStateRequest();
	}
}
