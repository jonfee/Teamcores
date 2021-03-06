using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TeamCores.Common.Json;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Events;
using TeamCores.Domain.Services.Request;

namespace TeamCores.Domain.Models.UserExam
{
	/// <summary>
	/// 用户考卷初始化业务验证失败结果枚举
	/// </summary>
	internal enum UserExamInitFailureRule
	{
		/// <summary>
		/// 参考请求不能为空
		/// </summary>
		[Description("参考请求不能为空")]
		INITREQUEST_CANNOT_EMPTY = 1,
		/// <summary>
		/// 考卷题目不能为空
		/// </summary>
		[Description("考卷题目不能为空")]
		QUESTIONS_CANNOT_EMPTY,
	}

	/// <summary>
	/// 用户考卷初始化业务领域
	/// </summary>
	internal class UserExamInit : EntityBase<long, UserExamInitFailureRule>
	{
		#region 属性

		private UserExamInitRequest request;
		/// <summary>
		/// 用户参考初始化请求
		/// </summary>
		public UserExamInitRequest Request
		{
			get
			{
				return request;
			}
		}

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化一个<see cref="UserExamInit"/>对象实例
		/// </summary>
		/// <param name="request"></param>
		public UserExamInit(UserExamInitRequest request)
		{
			if (request != null)
			{
				this.request = request;
				ID = request.UserExamId;
			}
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (Request == null)
			{
				AddBrokenRule(UserExamInitFailureRule.INITREQUEST_CANNOT_EMPTY);
			}
			else
			{
				if (request.QuestionsResults == null || request.QuestionsResults.Count < 1)
					AddBrokenRule(UserExamInitFailureRule.QUESTIONS_CANNOT_EMPTY);
			}
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 保存用户参考的考卷信息
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			ThrowExceptionIfValidateFailure();

			//题目及作答结果JSON
			string questionResultJson = JsonUtility.JsonSerializeObject(request.QuestionsResults);

			//用户参考试卷数据初始化
			var examUser = new Data.Entity.ExamUsers
			{
				Id = ID,
				ExamId = request.ExamId,
				UserId = request.UserId,
				CreateTime = request.CreateTime,
				Answer = questionResultJson,
				MarkingStatus = (int)ExamMarkingStatus.DIDNOT_READ,
				MarkingTime = null,
				PostTime = null,
				Times = 0,
				Total = 0
			};

			//**调用仓储服务存储，并调用考卷使用事件**
			// 1、存储
			bool success = ExamUsersAccessor.Insert(examUser);
			// 2、调用考卷使用事件
			if (success)
			{
				eventsChannels.Clear();

				eventsChannels.AddEvent(new ExamUsedEvent(new ExamUsedEventState { ExamId = request.ExamId }));

				eventsChannels.Execute();
			}

			return success;
		}

		#endregion
	}
}
