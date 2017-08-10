﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TeamCores.Common.Json;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Response;

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
		STUDENT_NOT_EXISTS
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
				var itemRightAnswer = rightAnswers.ContainsKey(item.QuestionId) ? rightAnswers[item.QuestionId] : string.Empty;

				//题目摘要数据对象
				var summary = new UserExamQuestionSummary(item);
				summary.RightAnswer = itemRightAnswer;

				yield return summary;
			}
		}

		#endregion
	}
}
