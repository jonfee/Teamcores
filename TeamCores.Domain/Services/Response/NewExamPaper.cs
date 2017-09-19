using System;
using System.Collections.Generic;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 考生摘要信息
	/// </summary>
	public class StudentSummary
	{
		/// <summary>
		/// 考生ID
		/// </summary>
		public long StudentId { get; set; }

		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// 手机
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 头衔
		/// </summary>
		public string Title { get; set; }
	}

	/// <summary>
	/// 用户考卷阅卷操作时的题目摘要信息
	/// </summary>
	public class UserExamQuestionSummary : UserExamQuestionResult
	{
		/// <summary>
		/// 正确答案（或参考答案）
		/// </summary>
		public string RightAnswer { get; set; }

		public UserExamQuestionSummary(UserExamQuestionResult result)
		{
			SortCode = result.SortCode;
			QuestionId = result.QuestionId;
			Type = result.Type;
			Topic = result.Topic;
            Answers = result.Answers;
			Score = result.Score;
			Result = result.Result;
			ActualScore = result.ActualScore;
		}
	}

	/// <summary>
	/// 用户考卷作答结果
	/// </summary>
	public class UserExamQuestionResult : ExamPaperQuestion
	{
		/// <summary>
		/// 该题的作答结果
		/// </summary>
		public string Result { get; set; }

		/// <summary>
		/// 实际得分
		/// </summary>
		public int ActualScore { get; set; }

		public UserExamQuestionResult() { }

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
		/// 考试时间（单位：分钟）
		/// </summary>
		public int Time { get; set; }

		/// <summary>
		/// 创建时间（考试开始时间）
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 试卷题目
		/// </summary>
		public List<ExamPaperQuestion> Questions { get; set; }
	}

	/// <summary>
	/// 用户考卷阅卷详细信息
	/// </summary>
	public class UserExamPaperMarkingDetails
	{
		/// <summary>
		/// 考试卷ID
		/// </summary>
		public long PaperId { get; set; }

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
		/// 考试时间限制（单位：分钟）
		/// </summary>
		public int Time { get; set; }

		/// <summary>
		/// 实际得分
		/// </summary>
		public int ActualTotal { get; set; }

		/// <summary>
		/// 创建时间，也是考试开始时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 交卷时间
		/// </summary>
		public DateTime? PostTime { get; set; }

		/// <summary>
		/// 实际答卷时间（单位：分钟）
		/// </summary>
		public int ActualTestTime
		{
			get
			{
				int minutes = 0;

				if (PostTime.HasValue)
				{
					minutes = unchecked((int)Math.Floor(PostTime.Value.Subtract(CreateTime).TotalMinutes));
				}

				return minutes;
			}
		}

		/// <summary>
		/// 阅卷状态
		/// </summary>
		public int MarkingStatus { get; set; }

		/// <summary>
		/// 阅卷时间
		/// </summary>
		public DateTime? MarkingTime { get; set; }

		/// <summary>
		/// 考生摘要信息
		/// </summary>
		public StudentSummary Student { get; set; }

		/// <summary>
		/// 试卷题目摘要信息
		/// </summary>
		public List<UserExamQuestionSummary> Questions { get; set; }
	}
}
