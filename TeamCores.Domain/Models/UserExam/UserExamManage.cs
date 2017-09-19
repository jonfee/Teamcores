using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Common.Json;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Response;
using TeamCores.Domain.Utility.AnswerDeserialize;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Models.UserExam
{
	/// <summary>
	/// 用户考卷阅卷业务验证失败结果枚举
	/// </summary>
	internal enum UserExamMarkingFailureRule
	{
		/// <summary>
		/// 用户考卷信息不存在
		/// </summary>
		[Description("用户考卷信息不存在")]
		USER_EXAM_NOT_EXISTS = 1,
		/// <summary>
		/// 考卷模板信息不存在
		/// </summary>
		[Description("考卷模板信息不存在")]
		EXAM_TEMPLATE_NOT_EXISTS,
		/// <summary>
		/// 考卷的题目及答案信息异常
		/// </summary>
		[Description("考卷的题目及答案信息异常")]
		QUESTION_SUMMARY_EXCEPTION,
		/// <summary>
		/// 学员信息不存在
		/// </summary>
		[Description("学员信息不存在")]
		STUDENT_NOT_EXISTS,
		/// <summary>
		/// 当前状态下不允许阅卷操作
		/// </summary>
		[Description("当前状态下不允许阅卷操作")]
		STATUS_CANNOT_MARKING,
		/// <summary>
		/// 阅卷的成绩存在数据异常
		/// </summary>
		[Description("阅卷的成绩存在数据异常")]
		MARKING_QUESTION_SCORES_EXCEPTION,
		/// <summary>
		/// 部分题目未提交阅卷结果
		/// </summary>
		[Description("部分题目未提交阅卷结果")]
		PARTOF_QUESTION_NO_SCORE,
		/// <summary>
		/// 题目最终得分不能大于题目的分值
		/// </summary>
		[Description("题目最终得分不能大于题目的分值")]
		QUESTION_LAST_SCORE_CANNOT_GREATERTHAN_SCORE
	}

	/// <summary>
	/// 用户考卷阅卷业务领域模型
	/// </summary>
	internal class UserExamManage : EntityBase<long, UserExamMarkingFailureRule>
	{
		#region 属性 

		private Data.Entity.ExamUsers userExam;
		/// <summary>
		/// 用户考卷信息
		/// </summary>
		public Data.Entity.ExamUsers UserExam
		{
			get
			{
				if (userExam == null)
				{
					userExam = ExamUsersAccessor.Get(ID);
				}

				return userExam;
			}
		}

		private Data.Entity.Exams exam;
		/// <summary>
		/// 用户考卷关联的模板ID
		/// </summary>
		public Data.Entity.Exams Exam
		{
			get
			{
				if (exam == null && UserExam != null)
				{
					exam = ExamsAccessor.Get(UserExam.ExamId);
				}

				return exam;
			}
		}

		private List<UserExamQuestionSummary> questionSummaryList;
		/// <summary>
		/// 考卷中所有题目的摘要信息
		/// </summary>
		public List<UserExamQuestionSummary> QuestionSummaryList
		{
			get
			{
				if (questionSummaryList == null && UserExam != null)
				{
					//题目答案结果信息
					var questionResultList = JsonUtility.JsonDeserialize<List<UserExamQuestionResult>>(UserExam.Answer);

					questionSummaryList = ConvertToQuestionSummary(questionResultList).ToList();
				}

				return questionSummaryList;
			}
		}

		private StudentSummary student;
		/// <summary>
		/// 考生摘要信息
		/// </summary>
		public StudentSummary Student
		{
			get
			{
				if (student == null && UserExam != null)
				{
					var user = UsersAccessor.Get(UserExam.UserId);

					if (user != null)
					{
						student = new StudentSummary
						{
							Email = user.Email,
							Mobile = user.Mobile,
							Name = user.Name,
							StudentId = user.UserId,
							Title = user.Title,
							UserName = user.Username
						};
					}
				}

				return student;
			}
		}

		#endregion

		#region 构造函数

		public UserExamManage(long userExamId)
		{
			ID = userExamId;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (UserExam == null)
			{
				AddBrokenRule(UserExamMarkingFailureRule.USER_EXAM_NOT_EXISTS);
			}
			else if (Exam == null)
			{
				AddBrokenRule(UserExamMarkingFailureRule.EXAM_TEMPLATE_NOT_EXISTS);
			}
			else if (Student == null)
			{
				AddBrokenRule(UserExamMarkingFailureRule.STUDENT_NOT_EXISTS);
			}
			else if (QuestionSummaryList == null || QuestionSummaryList.Count() < 1)
			{
				AddBrokenRule(UserExamMarkingFailureRule.QUESTION_SUMMARY_EXCEPTION);
			}
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 是否可以阅卷
		/// </summary>
		/// <returns></returns>
		public bool CanMarking()
		{
			return UserExam != null && UserExam.MarkingStatus == (int)ExamMarkingStatus.DIDNOT_READ;
		}

		/// <summary>
		/// 提交阅卷成绩
		/// </summary>
		/// <param name="QuestionScores"></param>
		/// <returns></returns>
		public bool SubmitMarking(Dictionary<long, int> QuestionScores)
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanMarking())
				{
					AddBrokenRule(UserExamMarkingFailureRule.STATUS_CANNOT_MARKING);
				}
				else if (QuestionScores == null || QuestionScores.Count() < 1)
				{
					AddBrokenRule(UserExamMarkingFailureRule.MARKING_QUESTION_SCORES_EXCEPTION);
				}
				else if (QuestionScores.Count() != QuestionSummaryList.Count())
				{
					AddBrokenRule(UserExamMarkingFailureRule.MARKING_QUESTION_SCORES_EXCEPTION);
				}

				//循环遍历每一题的得分并记录，检测到未提交得分或得分异常时抛出异常
				foreach (var item in QuestionSummaryList)
				{
					if (!QuestionScores.ContainsKey(item.QuestionId))
					{
						AddBrokenRule(UserExamMarkingFailureRule.PARTOF_QUESTION_NO_SCORE);
						break;
					}

					int score = QuestionScores[item.QuestionId];

					if (score > item.Score)
					{
						AddBrokenRule(UserExamMarkingFailureRule.QUESTION_LAST_SCORE_CANNOT_GREATERTHAN_SCORE);
						break;
					}

					item.ActualScore = score;
				}
			});

			//更新用户考卷信息
			userExam.MarkingStatus = (int)ExamMarkingStatus.READED;
			userExam.MarkingTime = DateTime.Now;
			userExam.Answer = JsonUtility.JsonSerializeObject(QuestionSummaryList);
			userExam.Total = QuestionSummaryList.Sum(p => p.ActualScore);

			//调用仓储服务更新
			bool success = ExamUsersAccessor.Update(UserExam);

			return success;
		}

		/// <summary>
		/// 获取用户答卷详细信息
		/// </summary>
		/// <returns></returns>
		public UserExamPaperMarkingDetails GetDetails()
		{
			ThrowExceptionIfValidateFailure();

			return new UserExamPaperMarkingDetails
			{
				ActualTotal = UserExam.Total,
				CreateTime = UserExam.CreateTime,
				MarkingStatus = UserExam.MarkingStatus,
				MarkingTime = UserExam.MarkingTime,
				PaperId = UserExam.Id,
				PostTime = UserExam.PostTime,
				Title = exam.Title,
				Time = exam.Time,
				Remarks = exam.Remarks,
				Pass = exam.Pass,
				ExamType = Exam.ExamType,
				Total = Exam.Total,
				Questions = QuestionSummaryList,
				Student = Student
			};
		}

		/// <summary>
		/// 转换数据为<see cref="UserExamQuestionSummary"/>类型数据
		/// </summary>
		/// <param name="results"></param>
		/// <returns></returns>
		private IEnumerable<UserExamQuestionSummary> ConvertToQuestionSummary(IEnumerable<UserExamQuestionResult> results)
		{
			if (results == null) yield break;

			//题目ID集合
			var questionIds = results.Select(p => p.QuestionId).ToArray();
			//获取各题的正确答案
			var rightAnswers = QuestionsAccessor.GetAnswersFor(questionIds);

			//循环处理数据类型，并添加上正确答案
			foreach (var item in results)
			{
				//本题的正确答案（或参考答案）
				var answerJsonData = rightAnswers.ContainsKey(item.QuestionId) ? rightAnswers[item.QuestionId] : string.Empty;

				//反序列化出答案（或解析）内容
				var deserContext = new AnswerDeserializeContext(item.Type);
				var answers = deserContext.Deserialize(answerJsonData);

				//过滤出正确的答案选项编号（或解析内容）
				var rightCodeArray = RightAnswersFilter(answers, item.Type);

				//题目摘要数据对象
				var summary = new UserExamQuestionSummary(item);
				summary.RightAnswer = rightCodeArray != null ? string.Join(",", rightCodeArray) : string.Empty;//将多个正确答案（或答案解析内容）用英文逗号分隔

				yield return summary;
			}
		}

		/// <summary>
		/// 从答案信息中过滤出正确的选项编号（或答案解析内容）
		/// </summary>
		/// <param name="answer"></param>
		/// <param name="questionType"></param>
		/// <returns></returns>
		private string[] RightAnswersFilter(QuestionAnswer answer, int questionType)
		{
			if (answer == null) return null;

			string[] rightResults = null;

			QuestionType type = (QuestionType)Enum.Parse(typeof(QuestionType), questionType.ToString());

			switch (type)
			{
				case QuestionType.SINGLE_CHOICE:
					var single = answer as SingleChoiceAnswer;
					rightResults = single.Options.Where(p => p.IsRight).Select(p => p.Code).ToArray();
					break;
				case QuestionType.MULTIPLE_CHOICE:
					var multiple = answer as MultipleChoiceAnswer;
					rightResults = multiple.Options.Where(p => p.IsRight).Select(p => p.Code).ToArray();
					break;
				case QuestionType.TRUE_OR_FALSE:
					var trueFalse = answer as TrueFalseAnswer;
					rightResults = new[] { trueFalse.Answer.ToString() };
					break;
				case QuestionType.GAP_FILLING:
					var filling = answer as GapFillingAnswer;
					rightResults = new[] { filling.Answer };
					break;
				case QuestionType.ESSAY_QUESTION:
					var essay = answer as EssayQuestionAnswer;
					rightResults = new[] { essay.Knowledge };
					break;
			}

			return rightResults;
		}

		#endregion
	}
}
