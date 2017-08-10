using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Common;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Infrastructure.ExamPager;
using TeamCores.Domain.Services.Response;
using TeamCores.Domain.Utility;

namespace TeamCores.Domain.Models.Exams
{
	/// <summary>
	/// 考卷编辑业务领域验证错误结果枚举
	/// </summary>
	internal enum ExamsManageFailureRule
	{
		/// <summary>
		/// 考卷对象不存在
		/// </summary>
		[Description("考卷对象不存在")]
		EXAMS_NOT_EXISTS = 1,
		/// <summary>
		/// 考卷状态不能设置为“启用”
		/// </summary>
		[Description("考卷状态不能设置为“启用”")]
		STATUS_CANNOT_SET_ENABLED,
		/// <summary>
		/// 考卷状态不能设置为“禁用”
		/// </summary>
		[Description("考卷状态不能设置为“禁用”")]
		STATUS_CANNOT_SET_DISABLED,
		/// <summary>
		/// 考卷编辑对象为空
		/// </summary>
		[Description("考卷编辑对象为空")]
		MODIFYSTATE_OBJECT_IS_NULL = 1,
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
	/// 考卷编辑状态数据
	/// </summary>
	public class ExamsModifyState
	{
		/// <summary>
		/// 考试类型（考试，练习）
		/// </summary>
		public int ExamType { get; set; }

		/// <summary>
		/// 关联的课程ID集合
		/// </summary>
		public string CourseIds { get; set; }

		/// <summary>
		/// 考卷标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 考试目标备注说明
		/// </summary>
		public string Remarks { get; set; }

		/// <summary>
		/// 考题ID集合
		/// </summary>
		public string Questions { get; set; }

		/// <summary>
		/// 考试时间（单位：分钟）
		/// </summary>
		public int Time { get; set; }

		/// <summary>
		/// 总分
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 及格分
		/// </summary>
		public int Pass { get; set; }

		/// <summary>
		/// 单选题目数量
		/// </summary>
		public int Radio { get; set; }

		/// <summary>
		/// 单选题总分
		/// </summary>
		public int RedioTotal { get; set; }

		/// <summary>
		/// 多选题目数量
		/// </summary>
		public int Multiple { get; set; }

		/// <summary>
		/// 多选总分
		/// </summary>
		public int MultipleTotal { get; set; }

		/// <summary>
		/// 判断题
		/// </summary>
		public int Judge { get; set; }

		/// <summary>
		/// 判断题总分
		/// </summary>
		public int JudgeTotal { get; set; }

		/// <summary>
		/// 填空题
		/// </summary>
		public int Filling { get; set; }

		/// <summary>
		/// 填空题总分
		/// </summary>
		public int FillingTotal { get; set; }

		/// <summary>
		/// 问答题数
		/// </summary>
		public int Ask { get; set; }

		/// <summary>
		/// 问答题总分
		/// </summary>
		public int AskTotal { get; set; }

		/// <summary>
		/// 可使用开始时间,null不限制
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 结束使用时间,null不限制
		/// </summary>
		public DateTime? EndTime { get; set; }

		public int Status { get; set; }
	}

	/// <summary>
	/// 考卷编辑业务领域模型
	/// </summary>
	internal class ExamsManage : EntityBase<long, ExamsManageFailureRule>
	{
		#region 属性

		private Data.Entity.Exams exams;

		/// <summary>
		/// 考卷信息
		/// </summary>
		public Data.Entity.Exams Exams
		{
			get
			{
				if (exams == null)
				{
					exams = ExamsAccessor.Get(ID);
				}

				return exams;
			}
		}

		private List<Data.Entity.Questions> questions;
		/// <summary>
		/// 题目集合
		/// </summary>
		public List<Data.Entity.Questions> Questions
		{
			get
			{
				if (questions == null && Exams != null)
				{
					var questionIds = Tools.TransferToLongArray(Exams.Questions);

					questions = QuestionsAccessor.GetAllFor(questionIds);
				}

				return questions;
			}
		}

		private List<Data.Entity.Course> courses;
		/// <summary>
		/// 关联的课程集合
		/// </summary>
		public List<Data.Entity.Course> Courses
		{
			get
			{
				if (courses == null && Exams != null)
				{
					var courseIds = Tools.TransferToLongArray(Exams.CourseIds);

					courses = CourseAccessor.GetList(courseIds);
				}

				return courses;
			}
		}

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化一个<see cref="ExamsManage"/>对象实例
		/// </summary>
		/// <param name="examsId">考卷ID</param>
		public ExamsManage(long examsId)
		{
			ID = examsId;
		}

		/// <summary>
		/// 初始化一个<see cref="ExamsManage"/>对象实例
		/// </summary>
		/// <param name="exams">考卷数据对象</param>
		public ExamsManage(Data.Entity.Exams exams)
		{
			if (exams != null)
			{
				this.exams = exams;
				ID = exams.ExamId;
			}
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (Exams == null) AddBrokenRule(ExamsManageFailureRule.EXAMS_NOT_EXISTS);
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 检测是否能被删除
		/// </summary>
		/// <returns></returns>
		public bool CanDelete()
		{
			return false;
		}

		/// <summary>
		/// 检测是否能被启用
		/// </summary>
		/// <returns></returns>
		public bool CanSetEnable()
		{
			return Exams != null && Exams.Status == (int)ExamsStatus.DISABLED;
		}

		/// <summary>
		/// 检测是否能被禁用
		/// </summary>
		/// <returns></returns>
		public bool CanSetDisable()
		{
			return Exams != null && Exams.Status == (int)ExamsStatus.ENABLED;
		}

		/// <summary>
		/// 检测是否能被编辑
		/// </summary>
		/// <returns></returns>
		public bool CanModify()
		{
			return true;
		}

		/// <summary>
		/// 设置状态为启用
		/// </summary>
		/// <returns></returns>
		public bool SetEnable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(ExamsManageFailureRule.STATUS_CANNOT_SET_ENABLED);
			});

			return ExamsAccessor.SetStatus(ID, (int)ExamsStatus.ENABLED);
		}

		/// <summary>
		/// 设置状态为禁用
		/// </summary>
		/// <returns></returns>
		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(ExamsManageFailureRule.STATUS_CANNOT_SET_DISABLED);
			});

			return ExamsAccessor.SetStatus(ID, (int)ExamsStatus.DISABLED);
		}

		/// <summary>
		/// 从<see cref="ExamsModifyState"/>对象更新考卷
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public bool ModifyTo(ExamsModifyState state)
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (state == null)
				{
					AddBrokenRule(ExamsManageFailureRule.MODIFYSTATE_OBJECT_IS_NULL);
				}
				else
				{
					//考卷标题为Empty
					if (string.IsNullOrWhiteSpace(state.Title)) AddBrokenRule(ExamsManageFailureRule.TITLE_CANNOT_EMPTY);

					//该考卷未指定题库
					var questionCount = Tools.TransferToLongArray(state.Questions).Count();
					if (questionCount < 1) AddBrokenRule(ExamsManageFailureRule.QUESTIONS_CANNOT_EMPTY);

					//及格分不能大于等于总分是不允许的
					if (state.Pass >= state.Total) AddBrokenRule(ExamsManageFailureRule.PASS_MUST_LESS_THAN_TO_TOTAL);

					//总分及各题分是否一致
					var totalSum = state.RedioTotal + state.MultipleTotal + state.JudgeTotal + state.FillingTotal + state.AskTotal;
					if (totalSum != state.Total) AddBrokenRule(ExamsManageFailureRule.TOTAL_NOT_EQUALS_SUM);

					//如果有设置考卷的使用有效时间，则开始时间必须小于结束时间
					if (state.StartTime.HasValue && state.EndTime.HasValue && state.StartTime.Value >= state.EndTime.Value)
					{
						AddBrokenRule(ExamsManageFailureRule.STARTTIME_MUST_BE_BEFORE_THE_ENDTIME);
					}
				}
			});

			//映射数据实体对象后存储
			var editExams = CreateNewExamsFor(state);

			return ExamsAccessor.Update(editExams);
		}

		/// <summary>
		/// 获取并转换为<see cref="ExamsDetails"/>类型数据对象
		/// </summary>
		/// <returns></returns>
		public ExamsDetails ConvertToExamsDetails()
		{
			if (Exams == null) return null;

			var questionIds = Tools.TransferToLongArray(Exams.Questions);
			var courseIds = Tools.TransferToLongArray(Exams.CourseIds);

			var details = new ExamsDetails
			{
				ExamId = Exams.ExamId,
				ExamType = Exams.ExamType,
				Title = Exams.Title,
				Remarks = Exams.Remarks,
				Time = Exams.Time,
				Total = Exams.Total,
				Pass = Exams.Pass,
				Status = Exams.Status,
				UserId = Exams.UserId,
				UseCount = Exams.UseCount,
				Answers = Exams.Answers,
				Radio = Exams.Radio,
				RedioTotal = Exams.RedioTotal,
				Multiple = Exams.Multiple,
				MultipleTotal = Exams.MultipleTotal,
				Judge = Exams.Judge,
				JudgeTotal = Exams.JudgeTotal,
				Filling = Exams.Filling,
				FillingTotal = Exams.FillingTotal,
				Ask = Exams.Ask,
				AskTotal = Exams.AskTotal,
				CreateTime = Exams.CreateTime,
				StartTime = Exams.StartTime,
				EndTime = Exams.EndTime,
				Questions = Questions,
				Courses = Courses
			};

			return details;
		}

		/// <summary>
		/// 从当前考卷模板中生成新试卷
		/// </summary>
		/// <returns></returns>
		public NewExamPaper CreateNewExamPaper()
		{
			ThrowExceptionIfValidateFailure();

			//考题生成上下文处理
			var context = new ExamPagerQuestionBuildContext(Exams, Questions);
			var firstState = new SingleChoiceQuestionsBuildState(context);
			context.SetState(firstState);

			//执行生成题目操作，并得到从当前考卷模板中生成的题目
			var questions = context.BuildQuestions();

			return new NewExamPaper
			{
				PaperId = IDProvider.NewId,
				ExamId = ID,
				ExamType = Exams.ExamType,
				Pass = Exams.Pass,
				Remarks = Exams.Remarks,
				Title = Exams.Title,
				Total = exams.Total,
				Questions = questions
			};
		}

		/// <summary>
		/// 生成一个新考卷对象
		/// </summary>
		/// <param name="state"></param>
		private Data.Entity.Exams CreateNewExamsFor(ExamsModifyState state)
		{
			var newExams = new Data.Entity.Exams
			{
				//不可修改内容
				ExamId = ID,
				Answers = Exams.Answers,
				CreateTime = Exams.CreateTime,
				Status = Exams.Status,
				UseCount = Exams.Status,
				UserId = Exams.UserId,
				//以下为修改内容
				ExamType = state.ExamType,
				CourseIds = state.CourseIds,
				Title = state.Title,
				Remarks = state.Remarks,
				Questions = state.Questions,
				Time = state.Time,
				Total = state.Total,
				Pass = state.Pass,
				Radio = state.Radio,
				RedioTotal = state.RedioTotal,
				Multiple = state.Multiple,
				MultipleTotal = state.MultipleTotal,
				Judge = state.Judge,
				JudgeTotal = state.JudgeTotal,
				Filling = state.Filling,
				FillingTotal = state.FillingTotal,
				Ask = state.Ask,
				AskTotal = state.AskTotal,
				StartTime = state.StartTime,
				EndTime = state.EndTime
			};

			return newExams;
		}

		#endregion
	}
}
