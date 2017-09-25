using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TeamCores.Common.Json;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Events;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Models.UserExam
{
	/// <summary>
	/// 考试卷交卷业务验证错误结果枚举
	/// </summary>
	internal enum SubmitExamPaperFailureRule
	{
		/// <summary>
		/// 交卷请求数据不能为空
		/// </summary>
		[Description("交卷请求数据不能为空")]
		REQUEST_CANNOT_NULL = 1,
		/// <summary>
		/// 用户考卷信息不存在
		/// </summary>
		[Description("用户考卷信息不存在")]
		USER_EXAM_NOT_EXISTS,
		/// <summary>
		/// 用户信息异常
		/// </summary>
		[Description("用户信息异常")]
		USER_INFO_EXCEPTION,
		/// <summary>
		/// 用户考卷信息题目异常
		/// </summary>
		[Description("用户考卷信息题目异常")]
		USER_EXAM_QUTIONS_EXCEPTION,
		/// <summary>
		/// 考题答案不能为空
		/// </summary>
		[Description("考题答案不能为空")]
		ANSWERS_RESULTS_CONNOT_EMPTY,
		/// <summary>
		/// 提交的答案数据不完整
		/// </summary>
		[Description("提交的答案数据不完整")]
		ANSWERS_RESULT_INCOMPLETE
	}

	/// <summary>
	/// 考试卷交卷业务领域模型
	/// </summary>
	internal class SubmitExamPaper : EntityBase<long, SubmitExamPaperFailureRule>
	{
		#region 属性

		private ExamPagerSubmitRequest request;
		/// <summary>
		/// 考卷作答结果提交请求
		/// </summary>
		public ExamPagerSubmitRequest Request
		{
			get { return request; }
		}

		private Data.Entity.ExamUsers userExam;
		/// <summary>
		/// 用户考卷信息
		/// </summary>
		public Data.Entity.ExamUsers UserExam
		{
			get
			{
				if (userExam == null && Request != null)
				{
					userExam = ExamUsersAccessor.Get(request.UserExamId);
				}

				return userExam;
			}
		}

		#endregion

		#region 构造函数 

		public SubmitExamPaper(ExamPagerSubmitRequest request)
		{
			if (request != null)
			{
				this.request = request;
				ID = request.UserExamId;
			}
		}

		#endregion

		#region  验证

		protected override void Validate()
		{
			if (Request == null)
			{
				AddBrokenRule(SubmitExamPaperFailureRule.REQUEST_CANNOT_NULL);
			}
			else if (UserExam == null)
			{
				AddBrokenRule(SubmitExamPaperFailureRule.USER_EXAM_NOT_EXISTS);
			}
			else if (UserExam.UserId != Request.UserId)
			{
				AddBrokenRule(SubmitExamPaperFailureRule.USER_INFO_EXCEPTION);
			}
			else if (Request.AnswerResults == null || Request.AnswerResults.Count < 1)
			{
				AddBrokenRule(SubmitExamPaperFailureRule.ANSWERS_RESULTS_CONNOT_EMPTY);
			}
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 提交考卷作答结果
		/// </summary>
		/// <returns></returns>
		public bool SubmitResult()
		{
			//原考卷的初始化的所有题目信息
			List<UserExamQuestionResult> questionResults = null;

			//验证业务数据规则
			ThrowExceptionIfValidateFailure(() =>
			{
				//解析出考卷的所有题目
				questionResults = JsonUtility.JsonDeserialize<List<UserExamQuestionResult>>(UserExam.Answer);

				if (questionResults == null || questionResults.Count() < 1)
				{
					AddBrokenRule(SubmitExamPaperFailureRule.USER_EXAM_QUTIONS_EXCEPTION);
				}
				else
				{
					//题目ID集合
					var questionIds = questionResults.Select(p => p.QuestionId).ToArray();
					//用户提交答案的题目ID集合
					var submitQuestionIds = request.AnswerResults.Keys;

					//交集数量
					var intersectCount = questionIds.Intersect(submitQuestionIds).Count();

					//交卷时的作答题目与考卷的题目不符时，视为错误的交卷
					if (intersectCount != questionIds.Count()) AddBrokenRule(SubmitExamPaperFailureRule.ANSWERS_RESULT_INCOMPLETE);
				}
			});

			//保存答题结果，并触发答题事件

			// 1、先逐题记录答案
			foreach (var item in questionResults)
			{
				//本题作答结果
				string userAnswer = Request.AnswerResults[item.QuestionId];

				//记录到当前题目的答题结果中
				item.Result = userAnswer;
			}

			// 2、保存答题结果
			userExam.Answer = JsonUtility.JsonSerializeObject(questionResults);
			userExam.PostTime = DateTime.Now;
			userExam.Times = unchecked((int)Math.Floor(DateTime.Now.Subtract(userExam.CreateTime).TotalMinutes));//交卷时间-考卷创建时间=答题的总分钟数
			bool success = ExamUsersAccessor.Update(UserExam);

			// 2、触发答题事件
			if (success)
			{
				eventsChannels.Clear();

				eventsChannels.AddEvent(new ExamUserSubmitEvent(new ExamUserSubmitEventState
				{
					UserExamId = Request.UserExamId,
					ExamId = UserExam.ExamId,
					UserId = Request.UserId
				}));

				eventsChannels.Execute();
			}

			return success;
		}

		#endregion
	}
}
