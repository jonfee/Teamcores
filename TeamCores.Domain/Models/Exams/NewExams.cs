using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Request;

namespace TeamCores.Domain.Models.Exams
{
	/// <summary>
	/// 新试卷模板领域业务验证错误结果枚举
	/// </summary>
	internal enum NewExamsFailureRule
	{
		/// <summary>
		/// 新考卷请求对象为空
		/// </summary>
		[Description("新考卷请求对象为空")]
		REQUEST_OBJECT_IS_NULL = 1,
		/// <summary>
		/// 考卷标题不能为空
		/// </summary>
		[Description("考卷标题不能为空")]
		TITLE_CANNOT_EMPTY,
		/// <summary>
		/// 考卷的题库不能为空
		/// </summary>
		[Description("考卷的题库不能为空")]
		QUESTIONS_CANNOT_EMPTY,
		/// <summary>
		/// 及格分必须小于总分
		/// </summary>
		[Description("及格分必须小于总分")]
		PASS_MUST_LESS_THAN_TO_TOTAL,
		/// <summary>
		/// 总分与各题型分值之和不一致
		/// </summary>
		[Description("总分与各题型分值之和不一致")]
		TOTAL_NOT_EQUALS_SUM,
		/// <summary>
		/// 开始时间必须在结束时间之前
		/// </summary>
		[Description("开始时间必须在结束时间之前")]
		STARTTIME_MUST_BE_BEFORE_THE_ENDTIME
	}

	/// <summary>
	/// 新试卷模板业务领域模型
	/// </summary>
	internal class NewExams : EntityBase<long, NewExamsFailureRule>
	{
		#region 属性

		/// <summary>
		/// 新考卷请求对象
		/// </summary>
		public NewExamsRequest Request { get; set; }

		/// <summary>
		/// 考卷状态
		/// </summary>
		public ExamsStatus Status => ExamsStatus.ENABLED;

		#endregion

		#region 构造函数

		public NewExams(NewExamsRequest request)
		{
			ID = IDProvider.NewId;
			Request = request;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//请求对象为NULL
			if (Request == null)
			{
				AddBrokenRule(NewExamsFailureRule.REQUEST_OBJECT_IS_NULL);
			}
			else
			{
				//考卷标题为Empty
				if (string.IsNullOrWhiteSpace(Request.Title)) AddBrokenRule(NewExamsFailureRule.TITLE_CANNOT_EMPTY);

				//该考卷未指定题库
				if (QuestionCount() < 1) AddBrokenRule(NewExamsFailureRule.QUESTIONS_CANNOT_EMPTY);

				//及格分不能大于等于总分是不允许的
				if (Request.Pass >= Request.Total) AddBrokenRule(NewExamsFailureRule.PASS_MUST_LESS_THAN_TO_TOTAL);

				//总分及各题分是否一致
				var totalSum = Request.RedioTotal + Request.MultipleTotal + Request.JudgeTotal + Request.FillingTotal + Request.AskTotal;
				if (totalSum != Request.Total) AddBrokenRule(NewExamsFailureRule.TOTAL_NOT_EQUALS_SUM);

				//如果有设置考卷的使用有效时间，则开始时间必须小于结束时间
				if (Request.StartTime.HasValue && Request.EndTime.HasValue && Request.StartTime.Value >= Request.EndTime.Value)
				{
					AddBrokenRule(NewExamsFailureRule.STARTTIME_MUST_BE_BEFORE_THE_ENDTIME);
				}
			}
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 保存考卷
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			ThrowExceptionIfValidateFailure();

			var exam = new Data.Entity.Exams
			{
				ExamId = ID,
				UserId = Request.UserId,
				ExamType = Request.ExamType,
				Title = Request.Title,
				Questions = Request.Questions,
				CourseIds = Request.CourseIds,
				Remarks = Request.Remarks,
				Time = Request.Time,
				Pass = Request.Pass,
				Total = Request.Total,
				Status = (int)Status,
				Radio = Request.Radio,
				RedioTotal = Request.RedioTotal,
				Multiple = Request.Multiple,
				MultipleTotal = Request.Multiple,
				Judge = Request.Judge,
				JudgeTotal = Request.JudgeTotal,
				Filling = Request.Filling,
				FillingTotal = Request.FillingTotal,
				Ask = Request.Ask,
				AskTotal = Request.AskTotal,
				CreateTime = DateTime.Now,
				StartTime = Request.StartTime,
				EndTime = Request.EndTime,
				Answers = 0,
				UseCount = 0
			};

			return ExamsAccessor.Insert(exam);
		}

		private int QuestionCount()
		{
			var ids = GetQuestions();

			return ids != null ? ids.Count() : 0;
		}

		/// <summary>
		/// 获取考卷的题库集合
		/// </summary>
		/// <returns></returns>
		private IEnumerable<long> GetQuestions()
		{
			if (Request == null || string.IsNullOrWhiteSpace(Request.Questions)) yield break;

			var strIds = Request.Questions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			long tempId = 0;

			foreach (var str in strIds)
			{
				if (long.TryParse(str, out tempId))
				{
					yield return tempId;
				}
			}
		}

		#endregion
	}
}
