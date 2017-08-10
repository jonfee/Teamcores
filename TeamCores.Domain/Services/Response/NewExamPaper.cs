using System.Collections.Generic;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 用户考卷作答结果
	/// </summary>
	public class UserExamQuestionResult : ExamPaperQuestion
	{
		/// <summary>
		/// 该题的作答结果
		/// </summary>
		public string Result { get; set; }

		public UserExamQuestionResult(ExamPaperQuestion question)
		{
			SortCode = question.SortCode;
			QuestionId = question.QuestionId;
			Type = question.Type;
			Topic = question.Topic;
			Score = question.Score;
			Answers = question.Answers;			
		}
	}

	/// <summary>
	/// 学员考卷的题目
	/// </summary>
	public class ExamPaperQuestion
	{
		/// <summary>
		/// 排序编号
		/// </summary>
		public int SortCode { get; set; }

		/// <summary>
		/// 题目ID
		/// </summary>
		public long QuestionId { get; set; }

		/// <summary>
		/// 题目类型
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// 题目标题
		/// </summary>
		public string Topic { get; set; }

		/// <summary>
		/// 该题分值
		/// </summary>
		public int Score { get; set; }

		/// <summary>
		/// 备选答案项集合
		/// </summary>
		public Dictionary<string, string> Answers { get; set; }
	}

	/// <summary>
	/// 从考卷模板中生成出的考试卷数据
	/// </summary>
	public class NewExamPaper
	{
		/// <summary>
		/// 考试卷ID
		/// </summary>
		public long PaperId { get; set; }

		/// <summary>
		/// 考卷模板ID
		/// </summary>
		public long ExamId { get; set; }

		/// <summary>
		/// 考卷类型（枚举：<see cref="ExamType"/>）
		/// </summary>
		public int ExamType { get; set; }

		/// <summary>
		/// 试卷标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 考试目标说明
		/// </summary>
		public string Remarks { get; set; }

		/// <summary>
		/// 总分
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 及格分
		/// </summary>
		public int Pass { get; set; }

		/// <summary>
		/// 试卷题目
		/// </summary>
		public List<ExamPaperQuestion> Questions { get; set; }
	}
}
